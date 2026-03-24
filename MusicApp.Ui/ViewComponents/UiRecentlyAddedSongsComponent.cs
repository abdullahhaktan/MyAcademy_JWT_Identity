using Microsoft.AspNetCore.Mvc;
using MusicAppUi.Services.BlogServices;
using MusicAppUi.Services.SongServices;

namespace MusicAppUi.ViewComponents
{
    public class UiRecentlyAddedSongsComponent(ISongService songService,IBlogService blogService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lastAddedSongs = await songService.GetRecentlyAddedSongsAsync();
            var lastBlogs = await blogService.GetLastBlogsAsync();
            ViewBag.lastBlogs = lastBlogs;
            return View(lastAddedSongs);
        }
    }
}
