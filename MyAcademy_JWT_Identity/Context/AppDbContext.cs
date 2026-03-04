using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyAcademy_JWT_Identity.Entities;

namespace MyAcademy_JWT_Identity.Context
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<AppUser, AppRole, int>(options)
    {
        public DbSet<Category> Categories { get; set; }
    }
}
