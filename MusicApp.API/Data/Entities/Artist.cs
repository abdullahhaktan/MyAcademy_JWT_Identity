namespace MusicApp.API.Data.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public string BioInfo { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }

        public List<Song> Songs { get; set; }
        public List<Album> Albums { get; set; }
    }
}
