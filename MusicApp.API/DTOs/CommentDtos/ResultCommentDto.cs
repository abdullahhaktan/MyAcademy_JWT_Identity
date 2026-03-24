using MusicApp.API.DTOs.BlogDtos;
using MusicApp.API.DTOs.UserDtos;

namespace MusicApp.API.DTOs.CommentDtos
{
    public class ResultCommentDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UserImage { get; set; }
        public DateTime CreatedDate { get; set; }

        public int BlogId { get; set; }
        public ResultBlogDto Blog { get; set; }

        public int MusicUserId { get; set; }
        public UserDto MusicUser { get; set; }
    }
}
