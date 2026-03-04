namespace MusicApp.API.DTOs.UserSongHistoryDtos
{
    public class CreateUserSongHistoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SongId { get; set; }
        public DateTime ListenedAt { get; set; } = DateTime.Now;
    }
}
