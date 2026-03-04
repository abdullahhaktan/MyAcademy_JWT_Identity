using MusicApp.API.DTOs.SongDtos;
using MusicApp.API.DTOs.UserDtos;

namespace MusicApp.API.DTOs.UserSongHistoryDtos
{
    public class ResultUserSongHistoryDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }

        public int SongId { get; set; }
        public ResultSongDto Song { get; set; }

        public DateTime ListenedAt { get; set; }
    }
}
