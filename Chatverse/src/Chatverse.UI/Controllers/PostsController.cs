using Chatverse.UI.ViewModels.Auth;
using Chatverse.UI.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Chatverse.UI.Controllers
{
    public class PostsController : Controller
    {
        private const string baseUrl = "http://localhost:5273/api";
        private readonly HttpClient _httpClient;

        public PostsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostViewModel createPostViewModel)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var jsonLogin = JsonConvert.SerializeObject(createPostViewModel);
            StringContent content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync($"{baseUrl}/Posts/CreatePost", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var message = responseMessage.Content.ReadAsStringAsync();
                return RedirectToAction("HomePage", "Main");
            }

            return RedirectToAction("HomePage", "Main");
        }
    }
}
