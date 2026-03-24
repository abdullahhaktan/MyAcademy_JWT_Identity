using MusicAppUi.DTOs.RecentlyAddedDtos;
using MusicAppUi.DTOs.SongDtos;
using MusicAppUi.DTOs.UserSongHistoryDtos;
using System.Net;
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

        public async Task<List<ResultSongDto>> GetAllSongsByDistinctCountryAsync()
        {
            var token = httpContextAccessor.HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await httpClient.GetAsync("api/Songs/GetAllSongsByDistinctCountry");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ResultSongDto>>();
            }

            return null; // Yetki hatası (401) veya başka bir hata varsa null döner
        }

        public async Task<List<ResultSongDto>> GetAllSongsByDistinctGenreAsync()
        {
            var token = httpContextAccessor.HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await httpClient.GetAsync("api/Songs/GetAllSongsByDistinctGenre");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ResultSongDto>>();
            }

            return null; // Yetki hatası (401) veya başka bir hata varsa null döner
        }

        public async Task<List<ResultSongDto>> GetFilteredSongsAsync(string sort, string country, string genre)
        {
            var token = httpContextAccessor.HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await httpClient.GetAsync($"api/Songs/GetFilteredSongs?sort={sort}&country={country}&genre={genre}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ResultSongDto>>();
            }

            return null; // Yetki hatası (401) veya başka bir hata varsa null döner
        }

        public async Task<List<ResultSongDto>> GetPopularSongsAsync()
        {

            var token = httpContextAccessor.HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await httpClient.GetAsync("api/Songs/GetPopularSongs");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ResultSongDto>>();
            }

            return null; // Yetki hatası (401) veya başka bir hata varsa null döner
        }

        public async Task<ResultSongDto> GetMostPopularRapAsync()
        {
            var token = httpContextAccessor.HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await httpClient.GetAsync("api/songs/GetMostPopularRap");

            // 1. Önce yanıtın başarılı (200-299 arası) olup olmadığına bak
            if (response.IsSuccessStatusCode)
            {
                // Sadece başarılıysa JSON'a çevir
                return await response.Content.ReadFromJsonAsync<ResultSongDto>();
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                // 404 ise null dön veya özel bir hata fırlat
                return null;
            }
            else
            {
                // Diğer hataları yönet (500, 401 vb.)
                throw new HttpRequestException($"API hata döndürdü: {response.StatusCode}");
            }

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

        public async Task<List<ResultSongDto>> GetRecommendedSongsAsync(int userId)
        {
            var token = httpContextAccessor.HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await httpClient.GetAsync($"api/Songs/GetRecommendedSongs/{userId}");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                
                if (string.IsNullOrWhiteSpace(content))
                    return new List<ResultSongDto>();

                return await response.Content.ReadFromJsonAsync<List<ResultSongDto>>();
            }

            return new List<ResultSongDto>();
        }
    }
}
