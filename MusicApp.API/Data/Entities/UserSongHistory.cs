namespace MusicApp.API.Data.Entities
{
    public class UserSongHistory
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int SongId { get; set; }
        public Song Song { get; set; }

        public DateTime ListenedAt { get; set; } = DateTime.Now;
    }
}
