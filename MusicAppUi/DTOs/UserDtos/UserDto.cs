using MusicAppUi.DTOs.PackageDtos;

namespace MusicAppUi.DTOs.UserDtos
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
