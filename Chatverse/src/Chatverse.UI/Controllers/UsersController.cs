using Chatverse.UI.DTOs.Post;
using Chatverse.UI.Services;
using Chatverse.UI.ViewModels.Post;
using Chatverse.UI.ViewModels.Settings;
using Chatverse.UI.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Chatverse.UI.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IFileService _fileService;
        public UsersController(HttpClient httpClient, IFileService fileService)
        {
            _httpClient = httpClient;
            _fileService = fileService;
        }

        private const string baseUrl = "http://localhost:5273/api";
        public async Task<GetAuthorShortInformationViewModel> ShortInformation()
        {
          
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Users/ShortInformation");
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync(); // JSON yanıtı string olarak al

                GetAuthorShortInformationViewModel infos = JsonConvert.DeserializeObject<GetAuthorShortInformationViewModel>(jsonResponse); // JSON'u List<string> olarak deserialize et
                return infos;
            }
            return null;
            
        }
        [HttpGet]
        public async Task<IActionResult> Media()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Settings/GetAllSocialMedia");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<SocialMediaViewModel> socialMediaViewModels = JsonConvert.DeserializeObject<List<SocialMediaViewModel>>(content);
                return View(model: socialMediaViewModels);
            }
            return RedirectToAction("AuthorProfile","Users");
        }

        [HttpGet]
        public async Task<IActionResult> AccountInformation()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Users/GetAccountInformation");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
               GetAccountInformationViewModel accountInformation = JsonConvert.DeserializeObject<GetAccountInformationViewModel>(content);
                return View(model: accountInformation);
            }
            return RedirectToAction("AuthorProfile", "Users");
        }
        [HttpPost]
        public async Task<IActionResult> AccountInformation(UpdateAccountInformationViewModel updateAccountInformationViewModel)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);


            string path = _fileService.FileUploadToRoot(updateAccountInformationViewModel.ProfilePicture);
            GetAccountInformationViewModel getAccountInformationViewModel = new GetAccountInformationViewModel();
            if (path is not null)
            {
                getAccountInformationViewModel.About = updateAccountInformationViewModel.About;
                getAccountInformationViewModel.Privicy = updateAccountInformationViewModel.Privicy;
                getAccountInformationViewModel.ProfilePicture = path;
                getAccountInformationViewModel.Fullname = updateAccountInformationViewModel.Fullname;
                getAccountInformationViewModel.Username = updateAccountInformationViewModel.Username;
                getAccountInformationViewModel.Email = updateAccountInformationViewModel.Email;
                
            }
            string jsonData = JsonConvert.SerializeObject(getAccountInformationViewModel);

            // İsteğin içeriğini ayarla
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"{baseUrl}/Users/UpdateInformation", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("AccountInformation", "Users");
            }

            return View(updateAccountInformationViewModel);
        }

        [HttpGet]
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
                GetAuthorShortInformationViewModel infos = await ShortInformation();
                ViewBag.InfoList = infos;
                return View(model: posts);
            }
            return View();
        }
    }
}
