using Chatverse.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Chatverse.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string baseUrl = "http://localhost:5273/api";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            if (accessToken == null) return RedirectToAction("Login","Auth");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync($"{baseUrl}/Posts/CreatePost");
            if (response.IsSuccessStatusCode)
            {
                var Message = await response.Content.ReadAsStringAsync();
                return View(model: Message);
            }
            return Error();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Comment()
        {
            return View();
        }
    }
}