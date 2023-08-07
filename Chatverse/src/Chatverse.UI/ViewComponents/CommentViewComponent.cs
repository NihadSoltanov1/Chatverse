using Microsoft.AspNetCore.Mvc;

namespace Chatverse.UI.ViewComponents
{
    public class CommentViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
