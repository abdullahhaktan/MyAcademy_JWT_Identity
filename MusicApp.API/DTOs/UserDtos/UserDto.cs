using MusicApp.API.DTOs.PackageDtos;

namespace MusicApp.API.DTOs.UserDtos
{
    public class UserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public int PackageId { get; set; }
        public ResultPackageDto Package { get; set; }
    }
}
