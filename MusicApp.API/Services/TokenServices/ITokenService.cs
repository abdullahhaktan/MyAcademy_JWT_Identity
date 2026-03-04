using MusicApp.API.Data.Entities;

namespace MusicApp.API.Services.TokenServices
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user);
    }
}
