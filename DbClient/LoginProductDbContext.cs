using LoginProductMinimalApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoginProductMinimalApi.DbClient
{
    public class LoginProductDbContext : DbContext
    {
        public LoginProductDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Client>()
                .Property(u => u.Id)
                .HasDefaultValueSql("uuid_generate_v4()");

            modelBuilder.Entity<Client>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("now()");
        }
    }
}
