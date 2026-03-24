using MusicAppUi.DTOs.UserDtos;

namespace MusicAppUi.Services.AccountServices
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task<UserDto> LoginAsync(LoginDto loginDto);
    }
}
