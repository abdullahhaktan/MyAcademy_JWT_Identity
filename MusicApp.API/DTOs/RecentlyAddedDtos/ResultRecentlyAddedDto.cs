using MusicApp.API.Data.Entities;
using MusicApp.API.DTOs.SongDtos;

namespace MusicApp.API.DTOs.SongPlayListDtos
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
