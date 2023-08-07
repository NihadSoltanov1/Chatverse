using Microsoft.AspNetCore.Mvc;

namespace Chatverse.UI.Controllers
{
    public class CommentsController : Controller
    {
        [HttpGet]
        public IActionResult GetComments()
        {
            return View();
        }
    }
}
