using Microsoft.AspNetCore.Mvc;
using MusicAppUi.Services.SongServices;

namespace MusicAppUi.ViewComponents
{
    public class UiRecentlyAddedSongsComponent(ISongService songService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lastAddedSongs = await songService.GetRecentlyAddedSongsAsync();
            return View(lastAddedSongs);
        }
    }
}
