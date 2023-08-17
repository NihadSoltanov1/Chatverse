using Chatverse.UI.DTOs.Error;
using Chatverse.UI.DTOs.Post;
using Chatverse.UI.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Chatverse.UI.Controllers
{
    public class SettingsController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "http://localhost:5273/api";
        public SettingsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            string jsonData = JsonConvert.SerializeObject(changePasswordViewModel);

            // İsteğin içeriğini ayarla
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{baseUrl}/Settings/ChangePassword", content);
            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Auth");
            }
            var responseMessage = await response.Content.ReadAsStringAsync();
            if(responseMessage.Contains("Message") || responseMessage.Contains("StatusCode") || responseMessage.Contains("Error"))
            {
                ErrorsContentDto error = JsonConvert.DeserializeObject<ErrorsContentDto>(responseMessage);
                ViewBag.Error = error;
            }
            else
            {
                List<ValidationErrorsViewModel> valError = JsonConvert.DeserializeObject<List<ValidationErrorsViewModel>>(responseMessage);
                ViewBag.valError = valError;
            }
         
            
            return View();
        }
        public IActionResult Properties()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            return View();
        }
    }
}
