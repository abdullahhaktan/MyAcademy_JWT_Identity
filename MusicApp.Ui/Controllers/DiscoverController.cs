using Microsoft.AspNetCore.Mvc;
using MusicApp.Ui.Filters;

namespace MusicAppUi.Controllers
{
    [AuthCheckFilter]
    public class DiscoverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}