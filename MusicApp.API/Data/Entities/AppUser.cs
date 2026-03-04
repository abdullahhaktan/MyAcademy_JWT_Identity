using Microsoft.AspNetCore.Identity;

namespace MusicApp.API.Data.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; }

        public int PackageId { get; set; }
        public Package Package { get; set; }
    }
}
