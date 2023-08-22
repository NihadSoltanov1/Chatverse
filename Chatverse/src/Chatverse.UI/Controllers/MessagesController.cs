using Chatverse.UI.DTOs.Message;
using Chatverse.UI.DTOs.Notification;
using Chatverse.UI.DTOs.SingleDto;
using Chatverse.UI.Services;
using Chatverse.UI.ViewModels.Friends;
using Chatverse.UI.ViewModels.Message;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace Chatverse.UI.Controllers
{
    public class MessagesController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "http://localhost:5273/api";
        private readonly IDateTimeConvertService _convertDate;
        public MessagesController(HttpClient httpClient, IDateTimeConvertService convertDate)
        {
            _httpClient = httpClient;
            _convertDate = convertDate;
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
                        SendDate = newDate.Hour
                    };
                    messages.Add(mesaj);
                }




                return Json(messages);
            }
            return NotFound();
        }

       
    }
}
