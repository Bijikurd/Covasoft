using LibraryData.Models;
using Microsoft.EntityFrameworkCore;


namespace LibraryData
{

    public class LibraryContext : DbContext
    {

        public LibraryContext(DbContextOptions options) : base(options) { }

        //object parton inlcuden boven aan als model zodat die gebruikt kan worden
        public DbSet<Patron> Patrons { get; set; }

    }
}
