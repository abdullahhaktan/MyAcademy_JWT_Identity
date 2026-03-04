using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApp.API.Data.Context;
using System.Security.Claims;

namespace MusicApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SongsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMySongs()
        {
            var packageIdClaim = User.Claims.FirstOrDefault(c => c.Type == "PackageId")?.Value;

            if (string.IsNullOrWhiteSpace(packageIdClaim))
            {
                return BadRequest("Paket bilginiz doğrulanamdı");
            }

            int userPackageId = int.Parse(packageIdClaim);

            var package = await _context.Packages.FindAsync(userPackageId);

            var songs = await _context.Songs.Where(s => s.Level <= package.Id).ToListAsync();

            return Ok(songs);
        }

        [HttpGet("GetLastListenedSongs")]
        public async Task<IActionResult> GetLastListenedSongs()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı bilginiz doğrulanamadı");
            }

            var lastListenedSongs = await _context.UserSongHistories.Include(uh=>uh.Song).ThenInclude(uh=>uh.Artist).OrderByDescending(sh => sh.ListenedAt).Take(5).ToListAsync();

            return Ok(lastListenedSongs);
        }

        [HttpGet("GetLastAddedSongs")]
        public async Task<IActionResult> GetLastAddedSongs()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı bilginiz doğrulanamadı");
            }

            var lastAddedSongs = await _context.RecentlyAddeds.Include(uh => uh.Song).ThenInclude(uh => uh.Artist).OrderByDescending(sh => sh.AddedDate).Take(5).ToListAsync();

            return Ok(lastAddedSongs);
        }
    }
}
