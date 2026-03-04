using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicApp.API.Data.Entities;

namespace MusicApp.API.Data.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Package> Packages { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<UserSongHistory> UserSongHistories { get; set; }
        public DbSet<RecentlyAdded> RecentlyAddeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Song>()
                .HasOne(s => s.Artist)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Song>()
                .HasOne(s => s.Album)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.AlbumId)
                .OnDelete(DeleteBehavior.Restrict);

            // teke tek ve çoka çok ilişki oluşturma yöntemleri (sadece örnek)

            //modelBuilder.Entity<AppUser>()
            //.HasOne(u => u.Profile)
            //.WithOne(p => p.User)
            //.HasForeignKey<UserProfile>(p => p.UserId);

            //modelBuilder.Entity<Song>()
            //.HasMany(s => s.Genres)
            //.WithMany(g => g.Songs)
            //.UsingEntity(j => j.ToTable("SongGenres")); // Ara tablo ismini sen belirlersin

        }
    }
}
