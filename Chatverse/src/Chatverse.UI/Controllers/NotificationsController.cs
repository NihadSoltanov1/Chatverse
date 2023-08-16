using Chatverse.UI.DTOs.Notification;
using Chatverse.UI.DTOs.SingleDto;
using Chatverse.UI.Services;
using Chatverse.UI.ViewModels.Notifications;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace Chatverse.UI.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IDateTimeConvertService _convertDate;
        private const string baseUrl = "http://localhost:5273/api";
        public NotificationsController(HttpClient httpClient, IDateTimeConvertService convertDate)
        {
            _httpClient = httpClient;
            _convertDate = convertDate;
        }
        [HttpGet]
        public async Task<IActionResult> GetNotification()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Notifications/GetCurrentUserNotification");
            if (response.IsSuccessStatusCode)
            {
                var notificationString = await response.Content.ReadAsStringAsync();
                List<GetUserNotificationViewModel> notifications = JsonConvert.DeserializeObject<List<GetUserNotificationViewModel>>(notificationString);
                List<GetUserNotificationDto> newNotifications = new List<GetUserNotificationDto>();
                foreach (var i in notifications)
                {
                    DateTimeDto newDate = _convertDate.Customize(i.CreateDate);
                    GetUserNotificationDto getUserNotificationDto = new GetUserNotificationDto()
                    {
                        CategoryName = i.CategoryName,
                        Content = i.Content,
                        DayYear = newDate.DayYear,
                        FullName = i.FullName,
                        Hour = newDate.Hour,
                        Icon = i.Icon,
                        Id = i.Id
                    };
                    newNotifications.Add(getUserNotificationDto);
                }
                return View(newNotifications);
            }
            return RedirectToAction("HomePage","Main");
        }
    }
}
