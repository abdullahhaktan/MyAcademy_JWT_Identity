using MusicAppUi.DTOs.ArtistDtos;
using MusicAppUi.DTOs.SongDtos;

namespace MusicAppUi.Services.ArtistServices
{
    public interface IArtistService
    {
        Task<List<ResultArtistDto>> GetAllArtistsAsync();
        Task<List<ResultArtistDto>> GetAllArtistsByDistinctCountryAsync();
        Task<List<ResultSongDto>> GetArtistTop5Tracks(int id);
        Task<List<ResultSongDto>> GetArtistOtherTracks(int id);
        Task<List<ResultArtistDto>> GetFilteredSongsAsync(string Country);
        Task<GetArtistByIdDto> GetArtistByIdAsync(int id);
        Task<int> GetTotalArtistCountAsync();
    }
}
