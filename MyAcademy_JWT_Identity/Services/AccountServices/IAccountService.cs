using MyAcademy_JWT_Identity.Dtos.UserDtos;

namespace MyAcademy_JWT_Identity.Services.AccountServices
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterDto registerDto);

        Task<string> LoginAsync(LoginDto loginDto);
    }
}
