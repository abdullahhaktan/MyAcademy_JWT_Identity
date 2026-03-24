using Microsoft.AspNetCore.Mvc;
using MusicApp.Ui.Filters;
using MusicAppUi.DTOs.SongDtos;
using MusicAppUi.Services.SongServices;

namespace MusicAppUi.Controllers
{
    [AuthCheckFilter]
    public class GenreController(ISongService songService) : Controller
    {
        // Sayfa açıldığında tüm şarkıları ve filtreleri getir
        public async Task<IActionResult> Index()
        {
            // Tüm şarkıları getir (popüler sıralama ile)
            var allSongs = await songService.GetFilteredSongsAsync("popular", "all", "all");

            // Benzersiz ülkeleri getir (her ülkeden bir örnek şarkı)
            var allCountries = await songService.GetAllSongsByDistinctCountryAsync();

            // Benzersiz türleri getir (her türden bir örnek şarkı)
            var allGenres = await songService.GetAllSongsByDistinctGenreAsync();

            // Popüler şarkıları da alabiliriz (isteğe bağlı)
            var popularSongs = await songService.GetPopularSongsAsync();

            ViewBag.allSongs = allSongs ?? new List<ResultSongDto>();
            ViewBag.allCountries = allCountries ?? new List<ResultSongDto>();
            ViewBag.allGenres = allGenres ?? new List<ResultSongDto>();
            ViewBag.popularSongs = popularSongs ?? new List<ResultSongDto>();


            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                // layout olmadan sadece içerik döndür
                //Response.Headers["X-No-Layout"] = "true";
                return PartialView("Index", allSongs);
            }

            // Model olarak tüm şarkıları gönder
            return View(allSongs ?? new List<ResultSongDto>());
        }

        // AJAX ile filtrelenmiş şarkıları getir
        [HttpGet]
        public async Task<IActionResult> Filter(string country, string genre)
        {
            // API'deki GetFilteredSongs endpoint'ini kullan
            // sort parametresini default olarak "popular" gönderiyoruz
            var filteredSongs = await songService.GetFilteredSongsAsync("popular", country ?? "all", genre ?? "all");

            return PartialView("_SongListPartial", filteredSongs ?? new List<ResultSongDto>());
        }

        // Sıralama seçeneği ile filtreleme (isterseniz ekleyebilirsiniz)
        [HttpGet]
        public async Task<IActionResult> FilterWithSort(string sort, string country, string genre)
        {
            var filteredSongs = await songService.GetFilteredSongsAsync(sort ?? "popular", country ?? "all", genre ?? "all");
            return PartialView("_SongListPartial", filteredSongs ?? new List<ResultSongDto>());
        }
    }
}