using MusicApp.API.DTOs.SongDtos;

namespace MusicApp.API.DTOs.ArtistDtos
{
    public class ResultArtistDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ResultSongDto> Songs { get; set; }
    }
}
