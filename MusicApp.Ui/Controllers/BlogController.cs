using Microsoft.AspNetCore.Mvc;
using MusicApp.Ui.Filters;
using MusicAppUi.DTOs.CommentDtos;
using MusicAppUi.Services.BlogServices;
using MusicAppUi.Services.SongServices;

namespace MusicAppUi.Controllers
{
    [AuthCheckFilter]
    public class BlogController(IBlogService blogService, ISongService songService) : Controller
    {
        private async Task GetViewBagDatas(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                ViewBag.message = true;
            }

            var mostPopularRap = await songService.GetMostPopularRapAsync();
            ViewBag.mostPopularRap = mostPopularRap.Title;
            ViewBag.mostPopularRapImageUrl = mostPopularRap.ImageUrl;

            var mostPopularSongs = await songService.GetPopularSongsAsync();
            ViewBag.mostPopularSongs = mostPopularSongs;

            var blogCommentCount = await blogService.GetBlogCommentCountAsync(id);
            ViewBag.blogCommentCount = blogCommentCount;

            var blogComments = await blogService.GetBlogCommentsAsync(id);
            ViewBag.blogComments = blogComments;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await blogService.GetAllBlogsAsync();
            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> BlogDetail(int id)
        {
            await GetViewBagDatas(id);
            var blog = await blogService.GetBlogByIdAsync(id);
            ViewBag.blogId = id;
            return View(blog);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(int blogId, string description)
        {
            var blog = await blogService.GetBlogByIdAsync(blogId);

            int? userId = HttpContext.Session.GetInt32("UserId");

            var userImage = HttpContext.Session.GetString("UserImage");

            CreateCommentDto createCommentDto = new CreateCommentDto()
            {
                BlogId = blogId,
                Description = description,
                MusicUserId = userId,
                UserImage = userImage
            };

            await blogService.CreateCommentAsync(createCommentDto);

            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(int blogId)
        {
            await GetViewBagDatas(blogId);
            var comments = await blogService.GetBlogCommentsAsync(blogId);
            return PartialView("CommentListPartial", comments);
        }
    }
}
