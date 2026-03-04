using Microsoft.AspNetCore.Mvc;
using MusicAppUi.Services.SongServices;

namespace MusicAppUi.ViewComponents
{
    public class UiLastListenedSongsComponent(ISongService songService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lastListenedSongs = await songService.GetLastListenedSongsAsync();
            return View(lastListenedSongs);
        }
    }
}
