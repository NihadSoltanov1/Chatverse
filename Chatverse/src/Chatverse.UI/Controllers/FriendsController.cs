using Chatverse.UI.DTOs.Post;
using Chatverse.UI.DTOs.SingleDto;
using Chatverse.UI.ViewModels.Friends;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

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

        [HttpGet]
        public async Task<IActionResult> AddFriend(string id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            IdDto id1 = new IdDto();
            id1.Id = id;
            var jsonLogin = JsonConvert.SerializeObject(id1);
            StringContent content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync($"{baseUrl}/Friendships/AddFriend", content);
           
            return Json(await responseMessage.Content.ReadAsStringAsync());
        }


        [HttpGet]
        public async Task<IActionResult> DeleteFriendRequest(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{baseUrl}/Friendships/DeleteFriendRequest/{id}");
            if (response.IsSuccessStatusCode)
            {
               string message = await response.Content.ReadAsStringAsync();
                return Json(message);
            };
            return NotFound();

        }




        [HttpGet]
        public async Task<IActionResult> SeeAllRequest()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Friendships/GetAllFriendRequest");
            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                List<GetAllFriendsRequestViewModel> getAllFriendsRequestViewModels = JsonConvert.DeserializeObject<List<GetAllFriendsRequestViewModel>>(message);
                return View(model: getAllFriendsRequestViewModels);
            }

            return RedirectToAction("HomePage","Main");
        }

    }
}
