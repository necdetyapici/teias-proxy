using Microsoft.EntityFrameworkCore;
using TeiasProxy.Models;

namespace TeiasProxy.Data
{
    public class ProxyDbContext : DbContext
    {
        public ProxyDbContext(DbContextOptions<ProxyDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProxyLog> ProxyLogs { get; set; }
        public DbSet<PlantCredentials> PlantCredentials { get; set; }
        public DbSet<LagosCredentials> LagosCredentials { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("teias");
            base.OnModelCreating(modelBuilder);
        }
    }
}
