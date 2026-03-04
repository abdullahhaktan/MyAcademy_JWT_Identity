using Microsoft.AspNetCore.Mvc;
using MusicAppUi.Services.SongServices;

namespace MusicAppUi.Controllers
{
    public class SongController(ISongService songService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var songs = await songService.GetAllSongsAsync();
            return View(songs);
        }
    }
}
