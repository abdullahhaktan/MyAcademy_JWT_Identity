using Microsoft.AspNetCore.Mvc;
using MusicApp.Ui.Filters;
using MusicAppUi.DTOs.SongDtos;
using MusicAppUi.Services.SongServices;

namespace MusicAppUi.Controllers
{
    [AuthCheckFilter]
    public class ChartController(ISongService songService) : Controller
    {
        public async Task<IActionResult> Index(
    string sort = "newest",
    string country = "all",
    string genre = "all")
        {
            var songs = await songService.GetFilteredSongsAsync(sort, country, genre)
                         ?? new List<ResultSongDto>();

            // FILTER AJAX request
            if (Request.Headers["X-Filter-Request"] == "true")
                return PartialView("FilteredSongsPartial", songs);

            // navbar ajax navigation request
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                await FillViewBagData();
                return PartialView("Index", songs);
            }

            await FillViewBagData();
            return View(songs);
        }

        private async Task FillViewBagData()
        {
            var mostPopularRap = await songService.GetMostPopularRapAsync();
            ViewBag.mostPopularRap = mostPopularRap.Title;
            ViewBag.mostPopularRapImageUrl = mostPopularRap.ImageUrl;

            ViewBag.mostPopularSongs = await songService.GetPopularSongsAsync();

            ViewBag.allSongs = await songService.GetAllSongsByDistinctCountryAsync();

            ViewBag.allGenres = await songService.GetAllSongsByDistinctGenreAsync();
        }

    }
}