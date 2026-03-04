namespace MusicApp.API.Data.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public List<Song> Songs { get; set; }
    }
}
