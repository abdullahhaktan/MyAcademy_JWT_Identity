
namespace MusicApp.API.DTOs.SongPlayListDtos
{
    public class UpdateRecentlyAddedDto
    {
        public int Id { get; set; }

        public int SongId { get; set; }

        public string Description { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}
