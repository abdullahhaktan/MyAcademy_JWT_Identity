namespace MusicAppUi.DTOs.CommentDtos
{
    public class CreateCommentDto
    {
        public string Description { get; set; }
        public string UserImage { get; set; }
        public int BlogId { get; set; }
        public int? MusicUserId { get; set; }
    }
}
