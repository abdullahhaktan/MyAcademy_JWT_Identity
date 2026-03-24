using MusicAppUi.DTOs.BlogDtos;
using MusicAppUi.DTOs.CommentDtos;

namespace MusicAppUi.Services.BlogServices
{
    public class BlogService(HttpClient httpClient) : IBlogService
    {
        public async Task<List<ResultBlogDto>> GetAllBlogsAsync()
        {
            var response = await httpClient.GetAsync("api/Blogs");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ResultBlogDto>>();
            }

            return null;
        }

        public async Task<List<ResultBlogDto>> GetLastBlogsAsync()
        {
            var response = await httpClient.GetAsync("api/Blogs/GetLastBlogs");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ResultBlogDto>>();
            }

            return null;
        }

        public async Task<GetBlogByIdDto> GetBlogByIdAsync(int id)
        {
            var response = await httpClient.GetAsync($"api/Blogs/GetBlogById/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GetBlogByIdDto>();
            }

            return null;
        }

        public async Task<int> GetBlogCommentCountAsync(int id)
        {
            var response = await httpClient.GetAsync($"api/Blogs/GetBlogCommentCount/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int>();
            }

            return 0;
        }

        public async Task<List<ResultCommentDto>> GetBlogCommentsAsync(int id)
        {
            var response = await httpClient.GetAsync($"api/Comments/GetBlogComments/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ResultCommentDto>>();
            }

            return null;
        }

        public async Task CreateCommentAsync(CreateCommentDto createCommentDto)
        {
            var response = await httpClient.PostAsJsonAsync($"api/Comments/CreateBlogComment/", createCommentDto);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Yorum eklenemedi. StatusCode: {response.StatusCode}");
            }

        }

    }
}
