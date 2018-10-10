using LibraryData.Models;
using Microsoft.EntityFrameworkCore;


namespace LibraryData
{

    public class LibraryContext : DbContext
    {

        public LibraryContext(DbContextOptions options) : base(options) { }

        public DbSet<Channels> Channels { get; set; }

        public DbSet<LibraryData.Models.System> Systems { get; set; }

        public DbSet<SystemStatus> SystemStatuses { get; set; }

        public DbSet<Triggers> Triggers { get; set; }

        public DbSet<Users> Users { get; set; }

    }
}
