using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApp.API.Data.Context;
using MusicApp.API.Data.Entities;
using MusicApp.API.DTOs.SongDtos;
using MusicApp.API.Services.RecommendationServices;
using System.Security.Claims;

namespace MusicApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRecommendationService _recommendationService;

        public SongsController(AppDbContext context , IRecommendationService recommendationService)
        {
            _context = context;
            _recommendationService = recommendationService;
        }


        private async Task<Package?> GetPackage()
        {
            var packageIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PackageId")?.Value;

            if (string.IsNullOrWhiteSpace(packageIdClaim))
                return null;

            if (!int.TryParse(packageIdClaim, out int userPackageId))
                return null;

            return await _context.Packages.FindAsync(userPackageId);
        }

        [HttpGet]
        public async Task<IActionResult> GetMySongs()
        {
            var package = await GetPackage();

            var songs = await _context.Songs
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .Where(s => package == null || s.Level <= package.Id)
                .ToListAsync();

            return Ok(songs);
        }

        [HttpGet("GetFilteredSongs")]
        public async Task<IActionResult> GetFilteredSongs([FromQuery] string sort, [FromQuery] string country, [FromQuery] string genre)
        {
            var package = await GetPackage();

            List<Song> filteredSongs = null;

            if (genre.ToLower() == "all" && country.ToLower() == "all")
            {
                if (sort.ToLower() == "newest")
                {
                    var startDate = DateTime.Now.AddDays(-14);
                    var endDate = DateTime.Now;
                    filteredSongs = await _context.Songs.Include(s => s.Artist).Include(s => s.Album)
                        .Where(s => (s.AddedDate >= startDate && s.AddedDate <= endDate))
                        .OrderByDescending(s => s.ClickCount).ToListAsync();
                }
                else
                {
                    filteredSongs = await _context.Songs.Include(s => s.Artist).Include(s => s.Album)
                        .OrderByDescending(s => s.ClickCount).ToListAsync();
                }
            }
            else if (country.ToLower() == "all")
            {
                if (sort.ToLower() == "newest")
                {
                    var startDate = DateTime.Now.AddDays(-14);
                    var endDate = DateTime.Now;
                    filteredSongs = await _context.Songs.Include(s => s.Artist).Include(s => s.Album)
                        .Where(s => s.Genre == genre && (s.AddedDate >= startDate && s.AddedDate <= endDate))
                        .OrderByDescending(s => s.ClickCount).ToListAsync();
                }
                else
                {
                    filteredSongs = await _context.Songs.Include(s => s.Artist).Include(s => s.Album)
                        .Where(s => s.Genre == genre)
                        .OrderByDescending(s => s.ClickCount).ToListAsync();
                }
            }
            else if (genre.ToLower() == "all")
            {
                if (sort.ToLower() == "newest")
                {
                    var startDate = DateTime.Now.AddDays(-14);
                    var endDate = DateTime.Now;
                    filteredSongs = await _context.Songs.Include(s => s.Artist).Include(s => s.Album)
                        .Where(s => s.Country.ToLower() == country.ToLower() && (s.AddedDate >= startDate && s.AddedDate <= endDate))
                        .OrderByDescending(s => s.ClickCount).ToListAsync();
                }
                else
                {
                    filteredSongs = await _context.Songs.Include(s => s.Artist).Include(s => s.Album)
                        .Where(s => s.Country.ToLower() == country.ToLower())
                        .OrderByDescending(s => s.ClickCount).ToListAsync();
                }
            }
            else
            {
                if (sort.ToLower() == "newest")
                {
                    var startDate = DateTime.Now.AddDays(-14);
                    var endDate = DateTime.Now;
                    filteredSongs = await _context.Songs.Include(s => s.Artist).Include(s => s.Album)
                        .Where(s => s.Country.ToLower() == country.ToLower() && s.Genre == genre && (s.AddedDate >= startDate && s.AddedDate <= endDate))
                        .OrderByDescending(s => s.ClickCount).ToListAsync();
                }
                else
                {
                    filteredSongs = await _context.Songs.Include(s => s.Artist).Include(s => s.Album)
                        .Where(s => s.Country.ToLower() == country.ToLower() && s.Genre == genre)
                        .OrderByDescending(s => s.ClickCount).ToListAsync();
                }
            }

            // ✅ BURASI DÜZELTİLDİ
            var values = filteredSongs.Where(s => package == null || s.Level < package.Id).Adapt<List<ResultSongDto>>();

            return Ok(values);
        }

        [HttpGet("GetAllSongsByDistinctCountry")]
        public async Task<IActionResult> GetAllSongsByDistinctCountry()
        {
            var package = await GetPackage();

            var songs = await _context.Songs
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .GroupBy(s => s.Country)
                .Select(g => g.FirstOrDefault())
                .ToListAsync();

            var values = songs.Adapt<List<ResultSongDto>>();

            // ✅ DÜZELTİLDİ
            var distinctCountrySongs = values
                .Where(s => package == null || s.Level < package.Id);

            return Ok(distinctCountrySongs);
        }

        [HttpGet("GetAllSongsByDistinctGenre")]
        public async Task<IActionResult> GetAllSongsByDistinctGenre()
        {
            var package = await GetPackage();

            var songs = await _context.Songs
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .GroupBy(s => s.Genre)
                .Select(g => g.FirstOrDefault())
                .ToListAsync();

            var values = songs.Adapt<List<ResultSongDto>>();

            // ✅ DÜZELTİLDİ
            var distinctGenreSongs = values
                .Where(s => package == null || s.Level < package.Id);

            return Ok(distinctGenreSongs);
        }

        [HttpGet("GetPopularSongs")]
        public async Task<IActionResult> GetPopularSongs()
        {
            var package = await GetPackage();

            var songs = await _context.Songs
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .OrderByDescending(s => s.ClickCount)
                .Take(6)
                .ToListAsync();

            // ✅ DÜZELTİLDİ
            var popularSongs = songs
                .Where(s => package == null || s.Level < package.Id);

            return Ok(popularSongs);
        }

        [HttpGet("GetMostPopularRap")]
        public async Task<IActionResult> GetMostPopularRap()
        {
            var song = await _context.Songs
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .OrderByDescending(s => s.ClickCount)
                .Where(s => s.Genre.ToLower() == "rock")
                .Take(1)
                .FirstOrDefaultAsync();

            if (song is null)
            {
                return NotFound();
            }

            return Ok(song);
        }

        [HttpGet("GetLastListenedSongs")]
        public async Task<IActionResult> GetLastListenedSongs()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı bilginiz doğrulanamadı");
            }

            var lastListenedSongs = await _context.UserSongHistories
                .Include(uh => uh.Song)
                .ThenInclude(uh => uh.Artist)
                .OrderByDescending(sh => sh.ListenedAt)
                .Take(5)
                .ToListAsync();

            return Ok(lastListenedSongs);
        }

        [HttpGet("GetLastAddedSongs")]
        public async Task<IActionResult> GetLastAddedSongs()
        {
            var package = await GetPackage();

            var lastAddedSongs = await _context.RecentlyAddeds
                .Include(uh => uh.Song)
                .ThenInclude(uh => uh.Artist)
                .OrderByDescending(sh => sh.AddedDate)
                .Where(uh => package == null || uh.Song.Level < package.Id)
                .Take(5)
                .ToListAsync();

            return Ok(lastAddedSongs);
        }

        [AllowAnonymous]
        [HttpGet("GetRecommendedSongs/{userId}")]
        public async Task<IActionResult> GetRecommendedSongs(int userId)
        {
            Console.WriteLine($">>> userId: {userId}");

            var recommendedSongIds = await _recommendationService.GetRecommendedSongIdsAsync(userId);

            Console.WriteLine($">>> Önerilen ID'ler: {string.Join(", ", recommendedSongIds)}");


            if (!recommendedSongIds.Any())
                return Ok(new List<ResultSongDto>());

            // 2. ID'lere göre şarkıları çek
            var songs = await _context.Songs
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .Where(s => recommendedSongIds.Contains(s.Id))
                .ToListAsync();

            var result = songs.Adapt<List<ResultSongDto>>();

            return Ok(result);
        }
    }
}