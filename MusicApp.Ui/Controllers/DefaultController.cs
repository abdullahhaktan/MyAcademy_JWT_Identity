using Microsoft.AspNetCore.Mvc;

namespace MusicAppUi.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
