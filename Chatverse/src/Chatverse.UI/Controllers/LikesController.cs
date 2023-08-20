using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Chatverse.UI.Controllers
{
    public class LikesController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "http://localhost:5273/api";
        public LikesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> LikePost(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Likes/LikePost/{id}");
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                return Json(message);
            };
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> UnlikePost(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{baseUrl}/Likes/UnlikePost/{id}");
            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                return Json(message);
            };
            return NotFound();
        }
    }
}
