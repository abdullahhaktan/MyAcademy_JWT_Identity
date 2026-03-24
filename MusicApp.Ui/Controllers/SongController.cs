using Microsoft.AspNetCore.Mvc;
using MusicApp.Ui.Filters;
using MusicAppUi.Services.SongServices;

namespace MusicAppUi.Controllers
{
    [AuthCheckFilter]
    public class SongController(ISongService songService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var songs = await songService.GetAllSongsAsync();
            return View(songs);
        }
    }
}
