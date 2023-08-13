using Microsoft.AspNetCore.Mvc;

namespace Chatverse.UI.Controllers
{
    public class FriendsController : Controller
    {
        public IActionResult FindFriend()
        {
            return View();
        }
    }
}
