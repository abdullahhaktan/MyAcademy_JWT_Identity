using MusicAppUi.DTOs.RecentlyAddedDtos;
using MusicAppUi.DTOs.SongDtos;
using MusicAppUi.DTOs.UserSongHistoryDtos;
using System.Net.Http.Headers;

namespace MusicAppUi.Services.SongServices
{
    public class SongService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : ISongService
    {
        public async Task<List<ResultSongDto>> GetAllSongsAsync()
        {
            var token = httpContextAccessor.HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await httpClient.GetAsync("api/Songs");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ResultSongDto>>();
            }

            return null; // Yetki hatası (401) veya başka bir hata varsa null döner
        }

        public async Task<List<ResultUserSongHistoryDto>> GetLastListenedSongsAsync()
        {
            var token = httpContextAccessor.HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await httpClient.GetAsync("api/Songs/GetLastListenedSongs");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ResultUserSongHistoryDto>>();
            }

            return null; // Yetki hatası (401) veya başka bir hata varsa null döner
        }

        public async Task<List<ResultRecentlyAddedDto>> GetRecentlyAddedSongsAsync()
        {
            var token = httpContextAccessor.HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await httpClient.GetAsync("api/Songs/GetLastAddedSongs");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ResultRecentlyAddedDto>>();
            }

            return null; // Yetki hatası (401) veya başka bir hata varsa null döner
        }
    }
}
