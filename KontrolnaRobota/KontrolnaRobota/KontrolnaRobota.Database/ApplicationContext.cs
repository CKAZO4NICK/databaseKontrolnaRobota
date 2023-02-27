
using KontrolnaRobota.Database.Configurations;
using KontrolnaRobota.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace KontrolnaRobota.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BuyerEntity> Buyers { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CheckEntity> Checks { get; set; }

        public ApplicationDbContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Database=KR;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CheckConfiguration());
            modelBuilder.ApplyConfiguration(new BuyerConfiguration());
        }
    }
}
