using MusicAppUi.DTOs.RecentlyAddedDtos;
using MusicAppUi.DTOs.SongDtos;
using MusicAppUi.DTOs.UserSongHistoryDtos;

namespace MusicAppUi.Services.SongServices
{
    public interface ISongService
    {
        Task<List<ResultSongDto>> GetAllSongsAsync();
        Task<List<ResultRecentlyAddedDto>> GetRecentlyAddedSongsAsync();
        Task<List<ResultUserSongHistoryDto>> GetLastListenedSongsAsync();
    }
}
