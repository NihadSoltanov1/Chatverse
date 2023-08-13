using Chatverse.UI.ViewModels.Friends;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chatverse.UI.Controllers
{
    public class FriendsController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "http://localhost:5273/api";

        public FriendsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> FindFriend()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Friends/FindFriend");
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync(); // JSON yanıtı string olarak al

                List<FindFriendsViewModel> users = JsonConvert.DeserializeObject<List<FindFriendsViewModel>>(jsonResponse); // JSON'u List<string> olarak deserialize et
                return View(model: users);
            }
            return RedirectToAction("HomePage","Main");
        }
    }
}
