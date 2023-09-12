using Chatverse.UI.DTOs.Auth;
using Chatverse.UI.DTOs.Error;
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
        [HttpGet]
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ValidEmailViewModel validEmailViewModel)
        {
            var jsonLogin = JsonConvert.SerializeObject(validEmailViewModel);
            StringContent content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{baseUrl}/Auth/ResetPassword", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login", "Auth");
            }
            string responseMessage = await response.Content.ReadAsStringAsync();
            if (responseMessage.Contains("Message") || responseMessage.Contains("StatusCode") || responseMessage.Contains("Error"))
            {
                ErrorsContentDto error = JsonConvert.DeserializeObject<ErrorsContentDto>(responseMessage);
                ViewBag.Error = error;
                return View();
            }
            else
            {
                List<ValidationErrorsViewModel> valError = JsonConvert.DeserializeObject<List<ValidationErrorsViewModel>>(responseMessage);
                ViewBag.valError = valError;
                return View();
            }
        }
        [HttpGet]
        [Route("auth/setnewpassword")]
        public async Task<IActionResult> SetNewPassword(string userId, string token)
        {
            ViewBag.Token = token;
            ViewBag.UserId = userId;
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> SetNewPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var jsonLogin = JsonConvert.SerializeObject(resetPasswordViewModel);
            StringContent content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{baseUrl}/Auth/SetNewPassword", content);
            if (response.IsSuccessStatusCode)
            {
                var jwt = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Login", "Auth");
            }
            string responseMessage = await response.Content.ReadAsStringAsync();
            if (responseMessage.Contains("Message") || responseMessage.Contains("StatusCode") || responseMessage.Contains("Error"))
            {
                ErrorsContentDto error = JsonConvert.DeserializeObject<ErrorsContentDto>(responseMessage);
                ViewBag.Error = error;
                return View();
            }
            else
            {
                List<ValidationErrorsViewModel> valError = JsonConvert.DeserializeObject<List<ValidationErrorsViewModel>>(responseMessage);
                ViewBag.valError = valError;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var jsonLogin = JsonConvert.SerializeObject(loginViewModel);
            StringContent content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{baseUrl}/Auth/Login", content);
            if (response.IsSuccessStatusCode)
            {
                var jwt = await response.Content.ReadAsStringAsync();

                if (jwt is null) return NotFound();
                HttpContext.Session.SetString("JWToken", jwt);

                return RedirectToAction("HomePage", "Main");
            }
            string responseMessage = await response.Content.ReadAsStringAsync();
            if (responseMessage.Contains("Message") || responseMessage.Contains("StatusCode") || responseMessage.Contains("Error"))
            {
                ErrorsContentDto error = JsonConvert.DeserializeObject<ErrorsContentDto>(responseMessage);
                ViewBag.Error = error;
                return View(loginViewModel);
            }
            else
            {
                List<ValidationErrorsViewModel> valError = JsonConvert.DeserializeObject<List<ValidationErrorsViewModel>>(responseMessage);
                ViewBag.valError = valError;
                return View(loginViewModel);
            }
        }

       


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            
            var jsonRegister = JsonConvert.SerializeObject(registerViewModel);
            StringContent content = new StringContent(jsonRegister, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{baseUrl}/Auth/Register", content);
            if (response.IsSuccessStatusCode) return RedirectToAction("CheckEmail");
            string responseMessage = await response.Content.ReadAsStringAsync();
            if (responseMessage.Contains("Message") || responseMessage.Contains("StatusCode") || responseMessage.Contains("Error"))
            {
                ErrorsContentDto error = JsonConvert.DeserializeObject<ErrorsContentDto>(responseMessage);
                ViewBag.Error = error;
            }
            else
            {
                List<ValidationErrorsViewModel> valError = JsonConvert.DeserializeObject<List<ValidationErrorsViewModel>>(responseMessage);
                ViewBag.valError = valError;
            }
            return View(registerViewModel);

        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            HttpContext.Session.Clear();  
            return RedirectToAction("Login", "Auth");

        }

        public async Task<IActionResult> Help()
        {
            return View();
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
            return NotFound();


        }
    }
}
