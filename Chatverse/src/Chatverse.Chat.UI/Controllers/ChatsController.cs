using Microsoft.AspNetCore.Mvc;

namespace Chatverse.Chat.UI.Controllers
{
    public class ChatsController : Controller
    {
        public IActionResult MainPage()
        {
            return View();
        }
    }
}
