using Microsoft.AspNetCore.Mvc;
using MusicAppUi.DTOs.SongDtos;
using MusicAppUi.Services.SongServices;
using System.Threading.Tasks;

namespace MusicAppUi.ViewComponents
{
    public class UiSuggestedSongsComponent(ISongService songService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return View(new List<ResultSongDto>());
            }
            var suggestedSongs = await songService.GetRecommendedSongsAsync(userId.Value);
            return View(suggestedSongs);
        }
    }
}
