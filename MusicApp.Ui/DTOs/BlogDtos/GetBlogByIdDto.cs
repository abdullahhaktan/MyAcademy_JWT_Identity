using MusicAppUi.DTOs.CommentDtos;

namespace MusicAppUi.DTOs.BlogDtos
{
    public class GetBlogByIdDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Description1 { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<ResultCommentDto> Comments { get; set; }
    }
}
