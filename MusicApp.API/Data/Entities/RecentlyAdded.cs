namespace MusicApp.API.Data.Entities
{
    public class RecentlyAdded
    {
        public int Id { get; set; }

        public int SongId { get; set; }
        public Song Song { get; set; }

        public string Description { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}
