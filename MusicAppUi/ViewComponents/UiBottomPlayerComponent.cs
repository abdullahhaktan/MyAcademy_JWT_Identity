using Microsoft.AspNetCore.Mvc;

namespace MusicAppUi.ViewComponents
{
    public class UiBottomPlayerComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
