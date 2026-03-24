using MusicAppUi.DTOs.RecentlyAddedDtos;
using MusicAppUi.DTOs.SongDtos;
using MusicAppUi.DTOs.UserSongHistoryDtos;

namespace MusicAppUi.Services.SongServices
{
    public interface ISongService
    {
        Task<List<ResultSongDto>> GetAllSongsAsync();
        Task<List<ResultSongDto>> GetAllSongsByDistinctCountryAsync();
        Task<List<ResultSongDto>> GetAllSongsByDistinctGenreAsync();
        Task<List<ResultSongDto>> GetFilteredSongsAsync(string sort, string country, string genre);
        Task<List<ResultSongDto>> GetPopularSongsAsync();
        Task<ResultSongDto> GetMostPopularRapAsync();
        Task<List<ResultRecentlyAddedDto>> GetRecentlyAddedSongsAsync();
        Task<List<ResultUserSongHistoryDto>> GetLastListenedSongsAsync();

        Task<List<ResultSongDto>> GetRecommendedSongsAsync(int userId);
    }
}
