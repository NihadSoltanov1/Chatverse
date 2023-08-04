using Microsoft.AspNetCore.Mvc;

namespace Chatverse.UI.Controllers
{
    public class UsersController : Controller
    {
       public async Task<IActionResult> AuthorProfile()
        {
            return View();
        }
    }
}
