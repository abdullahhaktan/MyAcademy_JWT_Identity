using Microsoft.AspNetCore.Mvc;

namespace MusicAppUi.Controllers
{
    public class DiscoverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
