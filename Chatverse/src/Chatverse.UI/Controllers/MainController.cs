using Chatverse.UI.ViewModels.Friends;
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
            {
                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/Posts/GetPostsByFriend");
                if (response.IsSuccessStatusCode)
                {

                    string jsonResponse = await response.Content.ReadAsStringAsync(); // JSON yanıtı string olarak al

                    List<GetPostsViewModel> posts = JsonConvert.DeserializeObject<List<GetPostsViewModel>>(jsonResponse); // JSON'u List<string> olarak deserialize et



                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    HttpResponseMessage response1 = await client.GetAsync($"{baseUrl}/Friendships/GetAllFriendRequest");
                    if (response1.IsSuccessStatusCode)
                    {
                        var message = await response1.Content.ReadAsStringAsync();
                        List<GetAllFriendsRequestViewModel> getAllFriendsRequestViewModels = JsonConvert.DeserializeObject<List<GetAllFriendsRequestViewModel>>(message);
                        List<GetAllFriendsRequestViewModel> takeFour = getAllFriendsRequestViewModels.Take(3).ToList();
                        ViewBag.friends = takeFour;
                    }

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    HttpResponseMessage response2 = await client.GetAsync($"{baseUrl}/Friendships/GetAllFriends");
                    if (response2.IsSuccessStatusCode)
                    {
                        var message1 = await response2.Content.ReadAsStringAsync();
                        List<GetAllFriendsViewModel> getAllFriends = JsonConvert.DeserializeObject<List<GetAllFriendsViewModel>>(message1);
                        List<GetAllFriendsViewModel> takePartFriend = getAllFriends.Take(3).ToList();
                        ViewBag.allFriends = takePartFriend;
                    }

                    return View(model: posts);
                }
                return View();
            }
        }
    }
}
