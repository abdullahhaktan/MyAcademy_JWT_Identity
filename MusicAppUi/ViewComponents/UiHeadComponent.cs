using Microsoft.AspNetCore.Mvc;

namespace MusicAppUi.ViewComponents
{
    public class UiHeadComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
