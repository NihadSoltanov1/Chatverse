using Chatverse.UI.DTOs.Message;
using Chatverse.UI.DTOs.Notification;
using Chatverse.UI.DTOs.Post;
using Chatverse.UI.DTOs.SingleDto;
using Chatverse.UI.Services;
using Chatverse.UI.ViewModels.Friends;
using Chatverse.UI.ViewModels.Message;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Chatverse.UI.Controllers
{
    public class MessagesController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "http://localhost:5273/api";
        private readonly IDateTimeConvertService _convertDate;
        private readonly IFileService _fileService;
        public MessagesController(HttpClient httpClient, IDateTimeConvertService convertDate, IFileService fileService)
        {
            _httpClient = httpClient;
            _convertDate = convertDate;
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody]object message)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            SendMessageViewModel sendMessage = JsonConvert.DeserializeObject<SendMessageViewModel>(message.ToString());
            var jsonLogin = JsonConvert.SerializeObject(sendMessage);
            StringContent content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync($"{baseUrl}/Messages/SendMessage", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = await responseMessage.Content.ReadAsStringAsync();
                return Json(response);
            }
            return  NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> VideoRoom([FromRoute]string id)
        {
            TempData["RoomId"] = id;
            return RedirectToAction("VideoCall");
        }

        [HttpGet]
        public async Task<IActionResult> VideoCall()
        {
            ViewBag.RoomId = (string)TempData["RoomId"];
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Friendships/GetAllFriends");
            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                List<GetAllFriendsViewModel> getAllFriends = JsonConvert.DeserializeObject<List<GetAllFriendsViewModel>>(message);
                ViewBag.Token = accessToken;
                return View(model: getAllFriends);
            }
            return RedirectToAction("HomePage", "Main");
        }


        [HttpPost]
        public async Task<IActionResult> UploadImageToRoot(IFormFile formFile)
        {
            IFormFile file = formFile;
            var filePath = _fileService.FileUploadToRoot(file, "MessageImages");
            return Json(filePath);
        }






        [HttpGet]
        public async Task<IActionResult> GetAllMessage([FromRoute]string id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login", "Auth");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Messages/GetMessage/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                List<GetAllMessageViewModel> messages1 = JsonConvert.DeserializeObject<List<GetAllMessageViewModel>>(responseContent);
                List<GetAllMessageDto> messages = new List<GetAllMessageDto>();

                foreach (var i in messages1)
                {
                    DateTimeDto newDate = _convertDate.Customize(i.SendDate);
                    GetAllMessageDto mesaj = new GetAllMessageDto()
                    {
                        Id = i.Id,
                        Content = i.Content,
                        ReceiverId = i.ReceiverId,
                        ReceiverUsername = i.ReceiverUsername,
                        SenderId = i.SenderId,
                        SenderUsername = i.SenderUsername,
                        ReceiverProfilePicture = i.ReceiverProfilePicture,
                        SenderProfilePicture = i.SenderProfilePicture,
                        SendDate = newDate.Hour,
                        Image = i.Image
                    };
                    messages.Add(mesaj);
                }
                return Json(messages);
            }
            return NotFound();

        }
    }
}
