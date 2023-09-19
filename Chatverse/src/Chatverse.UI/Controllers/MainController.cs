using Chatverse.UI.ViewModels.Friends;
using Chatverse.UI.ViewModels.Post;
using Chatverse.UI.ViewModels.Story;
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
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    HttpResponseMessage response3 = await client.GetAsync($"{baseUrl}/Stories/GetOwnStories");
                    if (response3.IsSuccessStatusCode)
                    {
                        var message1 = await response3.Content.ReadAsStringAsync();
                        List<GetOwnStoryViewModel> getAllOwnStories = JsonConvert.DeserializeObject<List<GetOwnStoryViewModel>>(message1);
                        List<GetOwnStoryViewModel> takePartOwnStory = getAllOwnStories.Take(1).ToList();
                        ViewBag.ownStories = takePartOwnStory;
                        ViewBag.AllOwnStories = getAllOwnStories;
                    }


                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    HttpResponseMessage response4 = await client.GetAsync($"{baseUrl}/Stories/GetFriendsStories");
                    if (response3.IsSuccessStatusCode)
                    {
                        var message2 = await response4.Content.ReadAsStringAsync();
                        List<GetFriendStoryViewModel> getAllFriendStories = JsonConvert.DeserializeObject<List<GetFriendStoryViewModel>>(message2);
                        List<GetFriendStoryViewModel> takePartFriendStory = getAllFriendStories.Take(2).ToList();
                        ViewBag.friendStories = takePartFriendStory;
                        ViewBag.AllFriendStories = getAllFriendStories;
                    }

                    return View(model: posts);
                }
                return View();
            }
        }
    }
}
