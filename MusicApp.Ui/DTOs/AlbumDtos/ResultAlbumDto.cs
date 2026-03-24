using MusicAppUi.DTOs.ArtistDtos;
using MusicAppUi.DTOs.SongDtos;

namespace MusicAppUi.DTOs.AlbumDtos
{
    public class ResultAlbumDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int ArtistId { get; set; }
        public ResultArtistDto Artist { get; set; }

        public List<ResultSongDto> Songs { get; set; }
    }
}
