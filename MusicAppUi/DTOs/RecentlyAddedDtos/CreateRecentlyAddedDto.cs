
namespace MusicAppUi.DTOs.RecentlyAddedDtos
{
    public class CreateRecentlyAddedDto
    {
        public int SongId { get; set; }

        public string Description { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}
