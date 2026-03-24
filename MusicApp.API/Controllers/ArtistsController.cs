using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApp.API.Data.Context;
using MusicApp.API.Data.Entities;
using MusicApp.API.DTOs.ArtistDtos;

namespace MusicApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var songs = await context.Artists.ToListAsync();
            return Ok(songs);
        }

        [HttpGet("GetAllArtistsByDistinctCountry")]
        public async Task<IActionResult> GetAllArtistsByDistinctCountry()
        {
            var values = await context.Artists.GroupBy(a => a.Country).Select(g => g.FirstOrDefault()).ToListAsync();

            var distinctArtistsByCountry = values.Adapt<List<ResultArtistDto>>();

            return Ok(distinctArtistsByCountry);
        }

        [HttpGet("GetFilteredArtists")]
        public async Task<IActionResult> GetFilteredArtists([FromQuery] string Country)
        {
            List<ResultArtistDto> filteredArtists = new List<ResultArtistDto>();

            var values = new List<Artist>();

            if (Country != "all")
            {
                values = await context.Artists.Where(a => a.Country == Country).ToListAsync();
            }
            else
            {
                values = await context.Artists.ToListAsync();
            }

            filteredArtists = values.Adapt<List<ResultArtistDto>>();

            return Ok(filteredArtists);
        }

        [HttpGet("GetTotalArtistCount")]
        public async Task<IActionResult> GetTotalArtistCount()
        {
            var totalArtistCount = await context.Artists.CountAsync();
            return Ok(totalArtistCount);
        }

        [HttpGet("GetArtistById/{id}")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            var artist = await context.Artists.Include(a => a.Songs).Where(a => a.Id == id).FirstOrDefaultAsync();
            return Ok(artist);
        }

        [HttpGet("GetArtistTopTracks/{id}")]
        public async Task<IActionResult> GetArtistTop5Tracks(int id)
        {
            var songs = await context.Songs.Include(s => s.Artist).Include(s => s.Album).Where(s => s.ArtistId == id).OrderByDescending(s => s.ClickCount).Take(5).ToListAsync();
            return Ok(songs);
        }

        [HttpGet("GetOtherArtistSongs/{id}")]
        public async Task<IActionResult> GetArtistTopOtherTracks(int id)
        {
            var songs = await context.Songs.Include(s => s.Artist).Include(s => s.Album).Where(s => s.ArtistId == id).OrderByDescending(s => s.ClickCount).Skip(5).Take(4).ToListAsync();
            return Ok(songs);
        }

    }
}
