using Chatverse.UI.DTOs.Post;
using Chatverse.UI.DTOs.Story;
using Chatverse.UI.Services;
using Chatverse.UI.ViewModels.Story;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Chatverse.UI.Controllers
{
    public class StoriesController : Controller
    {
        private const string baseUrl = "http://localhost:5273/api";
        private readonly HttpClient _httpClient;
        private readonly IFileService _fileService;

        public StoriesController(HttpClient httpClient, IFileService fileService)
        {
            _httpClient = httpClient;
            _fileService = fileService;
        }
        [HttpPost]
        public async Task<IActionResult> AddStory(AddStoryViewModel addStoryViewModel)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
           var filePath =  _fileService.FileUploadToRoot(addStoryViewModel.Media, "storyImages");
            AddStoryDto addStoryDto = new AddStoryDto()
            {
                Media = filePath
            };
            var jsonLogin = JsonConvert.SerializeObject(addStoryDto);
            StringContent content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{baseUrl}/Stories/AddStory", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("HomePage", "Main");
            }

            return NotFound();

        }


     
    }
}
