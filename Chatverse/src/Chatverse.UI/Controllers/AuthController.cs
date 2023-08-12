using Chatverse.UI.DTOs.Auth;
using Chatverse.UI.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Chatverse.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "http://localhost:5273/api";
        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken is not null) return RedirectToAction("HomePage", "Main");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View();
            var jsonLogin = JsonConvert.SerializeObject(loginViewModel);
            StringContent content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync($"{baseUrl}/Auth/Login", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jwt = await responseMessage.Content.ReadAsStringAsync();

                if (jwt is null) return NotFound();
                HttpContext.Session.SetString("JWToken", jwt);

                return RedirectToAction("HomePage", "Main");
            }

            return View(loginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
           
            string returnPath = String.Empty;
            if (registerViewModel.ProfilePicture != null && registerViewModel.ProfilePicture.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/profilepictures", registerViewModel.ProfilePicture.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                returnPath = "profilepictures/" + registerViewModel.ProfilePicture.FileName;
            }
            RegisterDto registerDto = new RegisterDto()
            {
                FullName = registerViewModel.FullName,
                Email = registerViewModel.Email,
                Password = registerViewModel.Password,
                Username = registerViewModel.Username,
                PasswordConfirm = registerViewModel.PasswordConfirm,
                ProfilePicture = returnPath
            };
            var jsonRegister = JsonConvert.SerializeObject(registerDto);
            StringContent content = new StringContent(jsonRegister, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync($"{baseUrl}/Auth/Register", content);


            if (responseMessage.IsSuccessStatusCode)
            {
                var message = await responseMessage.Content.ReadAsStringAsync();
                var s = "s";
                var r = "R";
                return Ok();
            }
            string c = await responseMessage.Content.ReadAsStringAsync();
            List<ValidationErrorsViewModel> errors = JsonConvert.DeserializeObject<List<ValidationErrorsViewModel>>(c);
            var a = "";
            var b = "";
            return Ok();

            //var r = "string";
            //return responseMessage.IsSuccessStatusCode ? RedirectToAction("CheckEmail", "Auth") : View(registerViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> CheckEmail()
        {
            return View();
        }
        [HttpGet]
        [Route("auth/confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            ConfirmMailDto confirmMailDto = new ConfirmMailDto();
            confirmMailDto.userId = userId;
            confirmMailDto.token = token;


            var jsonConfirmDto = JsonConvert.SerializeObject(confirmMailDto);
            StringContent content = new StringContent(jsonConfirmDto, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{baseUrl}/Auth/ConfirmEmail", content);
            if (response.IsSuccessStatusCode)
            {
                string responseMessage =await response.Content.ReadAsStringAsync();
                string deSerMessage = JsonConvert.DeserializeObject<string>(responseMessage);
                return Convert.ToBoolean(deSerMessage) ? RedirectToAction("Login", "Auth") : NotFound();
             }
            throw new Exception();


        }
    }
}
