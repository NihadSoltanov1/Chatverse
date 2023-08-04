using Chatverse.UI.ViewModels.Post;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
                string jsonResponse = await response.Content.ReadAsStringAsync(); // JSON yanıtı string olarak al

                List<GetPostsByFriendsViewModel> posts = JsonConvert.DeserializeObject<List<GetPostsByFriendsViewModel>>(jsonResponse); // JSON'u List<string> olarak deserialize et
                return View(model: posts);
            }
            return View();
        }
    }
}
