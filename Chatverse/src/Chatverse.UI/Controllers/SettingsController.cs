using Microsoft.AspNetCore.Mvc;

namespace Chatverse.UI.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Properties()
        {
            return View();
        }
    }
}
