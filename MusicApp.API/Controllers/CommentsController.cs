using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApp.API.Data.Context;
using MusicApp.API.Data.Entities;
using MusicApp.API.DTOs.CommentDtos;

namespace MusicApp.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController(AppDbContext context) : ControllerBase
    {
        [HttpGet("GetBlogComments/{id}")]
        public async Task<IActionResult> GetBlogComments(int id)
        {
            var values = await context.Comments.Include(c => c.MusicUser).Where(c => c.BlogId == id).ToListAsync();

            var blogComments = values.Adapt<List<ResultCommentDto>>();

            return Ok(blogComments);
        }

        [HttpPost("CreateBlogComment")]
        public async Task<IActionResult> CrateBlogComment(CreateCommentDto createCommentDto)
        {
            var comment = createCommentDto.Adapt<Comment>();
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
            return Ok(comment);
        }
    }
}
