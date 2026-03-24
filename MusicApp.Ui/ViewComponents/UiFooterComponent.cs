using Microsoft.AspNetCore.Mvc;

namespace MusicAppUi.ViewComponents
{
    public class UiFooterComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
