using washapp.services.customers.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace washapp.services.customers.infrastructure.Database
{
    public class WashAppDbContext : DbContext
    {
        public DbSet<Assortment> Assortments { get; set; }
        public DbSet<AssortmentCategory> AssortmentCategories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        
        public DbSet<Address>Addresses { get; set; }

        public WashAppDbContext(DbContextOptions<WashAppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("customers-service");
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder builder)
        //{
        //    builder.UseSqlServer("Server=DESKTOP-1L4G9FL;Database=WashAppDb;Trusted_Connection=True;");
        //}

    }
}
