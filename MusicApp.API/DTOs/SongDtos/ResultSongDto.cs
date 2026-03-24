using MusicApp.API.DTOs.AlbumDtos;
using MusicApp.API.DTOs.ArtistDtos;

namespace MusicApp.API.DTOs.SongDtos
{
    public class ResultSongDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Country { get; set; }
        public string FilePath { get; set; }
        public string ImageUrl { get; set; }
        public int Level { get; set; }
        public int ClickCount { get; set; }
        public DateTime AddedDate { get; set; }

        public int ArtistId { get; set; }
        public ResultArtistDto Artist { get; set; }

        public int AlbumId { get; set; }
        public ResultAlbumDto Album { get; set; }
    }
}
