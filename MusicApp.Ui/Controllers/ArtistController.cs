using Microsoft.AspNetCore.Mvc;
using MusicApp.Ui.Filters;
using MusicAppUi.DTOs.ArtistDtos;
using MusicAppUi.Services.ArtistServices;
using MusicAppUi.Services.SongServices;

namespace MusicAppUi.Controllers
{
    [AuthCheckFilter]
    public class ArtistController(IArtistService artistService, ISongService songService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var allArtistsByDistinctCountry = await artistService.GetAllArtistsByDistinctCountryAsync();
            ViewBag.allArtistsByDistinctCountry = allArtistsByDistinctCountry ?? new List<ResultArtistDto>();

            var allArtists = await artistService.GetAllArtistsAsync();
            ViewBag.allArtists = allArtists ?? new List<ResultArtistDto>();

            var totalArtistCount = await artistService.GetTotalArtistCountAsync();
            ViewBag.totalArtistCount = totalArtistCount;

            var mostPopularRap = await songService.GetMostPopularRapAsync();
            ViewBag.mostPopularRap = mostPopularRap.Title;
            ViewBag.mostPopularRapImageUrl = mostPopularRap.ImageUrl;

            var mostPopularSongs = await songService.GetPopularSongsAsync();
            ViewBag.mostPopularSongs = mostPopularSongs;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("Index", allArtists);
            }

            return View(allArtists);
        }

        [HttpGet]
        public async Task<IActionResult> Filter(string Country)
        {
            var filteredArtists = await artistService.GetFilteredSongsAsync(Country ?? "all");

            return PartialView("ArtistListPartial", filteredArtists ?? new List<ResultArtistDto>());
        }

        [HttpGet]
        public async Task<IActionResult> ArtistDetail(int id)
        {
            var artist = await artistService.GetArtistByIdAsync(id);

            var artistTop5Tracks = await artistService.GetArtistTop5Tracks(id);
            ViewBag.artistTop5Tracks = artistTop5Tracks;

            var otherSongs = await artistService.GetArtistOtherTracks(id);
            ViewBag.otherSongs = otherSongs;

            var mostPopularRap = await songService.GetMostPopularRapAsync();
            ViewBag.mostPopularRap = mostPopularRap.Title;

            var mostPopularSongs = await songService.GetPopularSongsAsync();
            ViewBag.mostPopularSongs = mostPopularSongs;

            return View(artist);
        }
    }
}
