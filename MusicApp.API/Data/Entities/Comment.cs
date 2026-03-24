namespace MusicApp.API.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UserImage { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int MusicUserId { get; set; }
        public AppUser MusicUser { get; set; }
    }
}
