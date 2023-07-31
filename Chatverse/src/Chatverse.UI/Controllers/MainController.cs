using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Chatverse.UI.Controllers
{
    public class MainController : Controller
    {
        private const string baseUrl = "http://localhost:5273/api";

        public async Task<IActionResult> HomePage()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync($"{baseUrl}/Posts/GetPostsByFriend");
            if (response.IsSuccessStatusCode)
            {
                var Message = await response.Content.ReadAsStringAsync();
                return View();
            }
            return Ok();
        }
    }
}
