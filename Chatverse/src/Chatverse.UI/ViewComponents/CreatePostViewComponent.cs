using Microsoft.AspNetCore.Mvc;

namespace Chatverse.UI.ViewComponents
{
    public class CreatePostViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
