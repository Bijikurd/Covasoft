using Microsoft.EntityFrameworkCore;
using MonitorAPI.DAL.Models;

namespace MonitorAPI.DAL
{

    public class MonitorContext : DbContext
    {
        public DbSet<Website> Websites { get; set; }

        public DbSet<Service> Services { get; set; }

        public MonitorContext(DbContextOptions<MonitorContext> options) : base(options)
        {
        }

        public MonitorContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = LAPTOP-VGDR86RO; Initial Catalog = Monitor; Integrated Security = SSPI");
        }
    }
}