using Microsoft.AspNetCore.Mvc;

namespace Chatverse.UI.Controllers
{
    public class MainController : Controller
    {
        public async Task<IActionResult> HomePage()
        {
            return View();
        }
    }
}
