using MusicAppUi.DTOs.SongDtos;

namespace MusicAppUi.DTOs.ArtistDtos
{
    public class ResultArtistDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ResultSongDto> Songs { get; set; }
    }
}
