using MusicApp.API.DTOs.ArtistDtos;
using MusicApp.API.DTOs.SongDtos;

namespace MusicApp.API.DTOs.AlbumDtos
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
