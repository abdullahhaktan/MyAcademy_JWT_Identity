namespace MusicApp.API.Data.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string ImageUrl { get; set; }
        public int Level { get; set; }


        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
