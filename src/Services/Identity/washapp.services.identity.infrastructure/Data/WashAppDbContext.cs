using washapp.services.identity.domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace washapp.services.identity.infrastructure.Data
{
    public class WashAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public WashAppDbContext(DbContextOptions<WashAppDbContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder builder)
        //{
        //    builder.UseSqlServer("Server=DESKTOP-1L4G9FL; Database=WashAppDb;Trusted_Connection=True;");

        //}


    }
}
