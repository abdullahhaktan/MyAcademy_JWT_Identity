using MusicApp.API.DTOs.AlbumDtos;
using MusicApp.API.DTOs.SongDtos;

namespace MusicApp.API.DTOs.ArtistDtos
{
    public class ResultArtistDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public string BioInfo { get; set; }

        public List<ResultSongDto> Songs { get; set; }

        public List<ResultAlbumDto> Albums { get; set; }
    }
}
