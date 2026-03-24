using Microsoft.AspNetCore.Identity;

namespace MusicApp.API.Data.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; }
        public string ImageUrl { get; set; }

        public int PackageId { get; set; }
        public Package Package { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
