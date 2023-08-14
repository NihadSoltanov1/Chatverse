using Chatverse.UI.ViewModels.Notifications;
using Chatverse.UI.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chatverse.UI.ViewComponents
{
    public class NotificationViewComponent : ViewComponent
    {
        private const string baseUrl = "http://localhost:5273/api";
        private readonly HttpClient _httpClient;

        public NotificationViewComponent(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
       
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"{baseUrl}/Notifications/GetCurrentUserNotification");
            if (response.IsSuccessStatusCode)
            {
                var notificationString = await response.Content.ReadAsStringAsync();
                List<GetUserNotificationViewModel> notifications = JsonConvert.DeserializeObject<List<GetUserNotificationViewModel>>(notificationString);

                ViewBag.Notifications = notifications;
            };
            return View();
            
        }
    }
}
