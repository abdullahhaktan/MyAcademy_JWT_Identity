using MyAcademy_JWT_Identity.Dtos.UserDtos;

namespace MyAcademy_JWT_Identity.Services.AccountServices
{
    public class AccountService(HttpClient httpClient) : IAccountService
    {
        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var response = await httpClient.PostAsJsonAsync("api/Login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                // API'den gelen token string'ini oku
                return await response.Content.ReadAsStringAsync();
            }

            return "Sunucu Hatası";
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            var response = await httpClient.PostAsJsonAsync("api/Register", registerDto);
            return response.IsSuccessStatusCode;
        }
    }
}
