using MusicAppUi.DTOs.BlogDtos;
using MusicAppUi.DTOs.CommentDtos;

namespace MusicAppUi.Services.BlogServices
{
    public interface IBlogService
    {
        Task<List<ResultBlogDto>> GetAllBlogsAsync();
        Task<List<ResultBlogDto>> GetLastBlogsAsync();
        Task<List<ResultCommentDto>> GetBlogCommentsAsync(int id);
        Task<GetBlogByIdDto> GetBlogByIdAsync(int id);
        Task<int> GetBlogCommentCountAsync(int id);
        Task CreateCommentAsync(CreateCommentDto createCommentDto);
    }
}
