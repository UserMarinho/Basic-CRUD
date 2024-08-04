using Microsoft.EntityFrameworkCore;
using DBConnection.Entities;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

namespace DBConnection.Database
{
    class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionStrings = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionStrings, new MySqlServerVersion(new Version(8, 0, 37)));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasIndex(p => p.CPF)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
