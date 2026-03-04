using MusicAppUi.DTOs.UserDtos;

namespace MusicAppUi.Services.AccountServices
{
    public class AccountService(HttpClient httpClient) : IAccountService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var response = await httpClient.PostAsJsonAsync("api/Login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                // API'den gelen token string'ini oku
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }

            return null;
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            var response = await httpClient.PostAsJsonAsync("api/Register", registerDto);
            return response.IsSuccessStatusCode;
        }
    }
}
