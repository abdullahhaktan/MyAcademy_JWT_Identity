using Microsoft.AspNetCore.Mvc;

namespace MusicAppUi.ViewComponents
{
    public class UiNavbarComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
