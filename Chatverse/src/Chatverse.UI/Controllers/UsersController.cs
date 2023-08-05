using Chatverse.UI.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chatverse.UI.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _httpClient;

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private const string baseUrl = "http://localhost:5273/api";

        public async Task<IActionResult> AuthorProfile()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Users/GetAuthorPosts");
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync(); // JSON yanıtı string olarak al

                List<GetPostsViewModel> posts = JsonConvert.DeserializeObject<List<GetPostsViewModel>>(jsonResponse); // JSON'u List<string> olarak deserialize et
                return View(model: posts);
            }
            return View();
        }
    }
}
