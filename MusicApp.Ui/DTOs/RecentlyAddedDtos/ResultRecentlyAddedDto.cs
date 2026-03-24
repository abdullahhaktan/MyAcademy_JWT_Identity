
using MusicAppUi.DTOs.SongDtos;

namespace MusicAppUi.DTOs.RecentlyAddedDtos
{
    public class ResultRecentlyAddedDto
    {
        public int Id { get; set; }

        public int SongId { get; set; }
        public ResultSongDto Song { get; set; }

        public string Description { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}
