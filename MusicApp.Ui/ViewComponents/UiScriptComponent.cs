using Microsoft.AspNetCore.Mvc;

namespace MusicAppUi.ViewComponents
{
    public class UiScriptComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
