using Chatverse.UI.ViewModels.Friends;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace Chatverse.UI.Controllers
{
    public class MessagesController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "http://localhost:5273/api";
        public MessagesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
           
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Friendships/GetAllFriends");
            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                List<GetAllFriendsViewModel> getAllFriends = JsonConvert.DeserializeObject<List<GetAllFriendsViewModel>>(message);
                ViewBag.Token = accessToken;
                return View(model: getAllFriends);
            }
            return RedirectToAction("HomePage", "Main");
        }

       
    }
}
