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
            if (accessToken is not null) return RedirectToAction("Index","Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var jsonLogin = JsonConvert.SerializeObject(loginViewModel);
            StringContent content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync($"{baseUrl}/Auth/Login", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jwt = await responseMessage.Content.ReadAsStringAsync();

                if (jwt is null) return NotFound();
                HttpContext.Session.SetString("JWToken", jwt);

                return RedirectToAction("Index", "Home");
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
            if (!ModelState.IsValid)
            {
                return View();
            }
            var jsonRegister = JsonConvert.SerializeObject(registerViewModel);
            StringContent content = new StringContent(jsonRegister, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync($"{baseUrl}/Auth/Register", content);
            return responseMessage.IsSuccessStatusCode ? RedirectToAction("Login", "Auth") : View(registerViewModel);
        }
    }
}
