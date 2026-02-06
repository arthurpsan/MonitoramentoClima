using Microsoft.EntityFrameworkCore;
using MonitoramentoClima.API.Models;

namespace MonitoramentoClima.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<Reading> Readings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        
    }
}
