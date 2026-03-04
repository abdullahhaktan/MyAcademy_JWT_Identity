using MusicApp.API.DTOs.UserDtos;

namespace MusicApp.API.DTOs.PackageDtos
{
    public class ResultPackageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserDto> Users { get; set; }
    }
}
