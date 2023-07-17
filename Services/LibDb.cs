using GameLibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryApi.Services
{
    public class LibDb : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public LibDb(DbContextOptions<LibDb> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
