using MusicAppUi.DTOs.UserDtos;

namespace MusicAppUi.DTOs.PackageDtos
{
    public class ResultPackageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserDto> Users { get; set; }
    }
}
