using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApp.API.Data.Context;
using MusicApp.API.DTOs.BlogDtos;

namespace MusicApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await context.Blogs.ToListAsync();
            return Ok(blogs);
        }

        [HttpGet("GetLastBlogs")]
        public async Task<IActionResult> GetLastBLogs()
        {
            var lastBlogs = await context.Blogs.Take(3).ToListAsync();
            return Ok(lastBlogs);
        }

        [HttpGet("GetBlogById/{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var value = await context.Blogs.FindAsync(id);
            var blog = value.Adapt<ResultBlogDto>();
            return Ok(blog);
        }

        [HttpGet("GetBlogCommentCount/{id}")]
        public async Task<IActionResult> GetBlogCommentCount(int id)
        {
            var value = await context.Blogs.Where(b => b.Id == id).CountAsync();
            return Ok(value);
        }
    }
}
