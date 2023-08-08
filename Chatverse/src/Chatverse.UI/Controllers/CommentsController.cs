using Chatverse.UI.DTOs.Post;
using Chatverse.UI.ViewModels.Comments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http;
using System.Text;

namespace Chatverse.UI.Controllers
{
    public class CommentsController : Controller
    {
        private const string baseUrl = "http://localhost:5273/api";
        private readonly HttpClient _httpClient;

        public CommentsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentViewModel createCommentViewModel)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null)
            {
                return Json("false");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var jsonComment = JsonConvert.SerializeObject(createCommentViewModel);
            StringContent content = new StringContent(jsonComment, Encoding.UTF8, "application/json");

            var responseMessage = await _httpClient.PostAsync($"{baseUrl}/Comments/CreateComment", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                var message = await responseMessage.Content.ReadAsStringAsync();
                return Json(message);
            }
            else
            {
                return Json("false");
            }
        }

    }
}
