using Chatverse.UI.DTOs.Post;
using Chatverse.UI.DTOs.PostFile;
using Chatverse.UI.Services;
using Chatverse.UI.ViewModels.Auth;
using Chatverse.UI.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Chatverse.UI.Controllers
{
    public class PostsController : Controller
    {
        private const string baseUrl = "http://localhost:5273/api";
        private readonly HttpClient _httpClient;
        private readonly IFileService _fileService;
        public PostsController(HttpClient httpClient, IFileService fileService)
        {
            _httpClient = httpClient;
            _fileService = fileService;
        }
        [HttpGet]
        public async Task<IActionResult> DeletePost(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{baseUrl}/Posts/DeletePost/{id}");
            if (response.IsSuccessStatusCode)
            {
                var filePathString =await response.Content.ReadAsStringAsync();
                _fileService.FileDeleteFromRoot(filePathString);
                return RedirectToAction("AuthorProfile", "Users");  
            };
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostViewModel createPostViewModel)
        {

            List<string> paths = _fileService.FileUploadToRoot(createPostViewModel.Media);
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


        [HttpGet]
        public async Task<IActionResult> UpdatePost(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Posts/GetPostById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var postString = await response.Content.ReadAsStringAsync();
                GetPostByIdViewModel post = JsonConvert.DeserializeObject<GetPostByIdViewModel>(postString);

                return View(model: post);
            };
            return RedirectToAction("AuthorProfile","Users");
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePost(UpdatePostViewModel updatePostViewModel)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            
            List<string> paths = _fileService.FileUploadToRoot(updatePostViewModel.UpdateMedia);


            UpdatePostDto updatePostDto = new UpdatePostDto()
            {
                UpdateContent = updatePostViewModel.UpdateContent,
                UpdateMedia = paths,
                UpdatePostId = updatePostViewModel.UpdatePostId
            };

            string jsonData = JsonConvert.SerializeObject(updatePostDto);

            // İsteğin içeriğini ayarla
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{baseUrl}/Posts/UpdatePost", content);
            if (response.IsSuccessStatusCode)
            {
                string oldPath = await response.Content.ReadAsStringAsync();
                _fileService.FileDeleteFromRoot(oldPath);
                return RedirectToAction("AuthorProfile", "Users");
            }
            
            return View(updatePostViewModel);
        }

     
    }
}
