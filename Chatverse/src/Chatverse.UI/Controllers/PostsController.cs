using Chatverse.UI.DTOs.Post;
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
        private readonly IWebHostEnvironment _env;
        public PostsController(HttpClient httpClient, IWebHostEnvironment env)
        {
            _httpClient = httpClient;
            _env = env;
        }


        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostViewModel createPostViewModel)
        {

            List<string> paths = new List<string>();
            foreach(IFormFile file in createPostViewModel.Media)
            if (file != null && file.Length > 0)
            {
                    var fileUniqueName =  DateTime.Now.ToString("yyyymmddMMss") + "_" + Path.GetFileName(file.FileName);
                    var folderPath = Path.Combine(_env.WebRootPath, "postfiles");
                    var fullPath = Path.Combine(folderPath, fileUniqueName);
                    string rootFolder = @"wwwroot\";
                    string returnPath = fullPath.Substring(fullPath.IndexOf(rootFolder, StringComparison.OrdinalIgnoreCase) + rootFolder.Length).Replace("\\", "/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    Directory.CreateDirectory(folderPath);
                    using (FileStream fs = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(fs);
                    }
                    paths.Add(fullPath);
                }
            CreatePostDto createPostDto = new CreatePostDto()
            {
                Content = createPostViewModel.Content,
                MediaLocation = paths
            };
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var jsonLogin = JsonConvert.SerializeObject(createPostDto);
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
