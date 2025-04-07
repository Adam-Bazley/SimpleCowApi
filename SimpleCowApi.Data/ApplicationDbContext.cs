using Microsoft.EntityFrameworkCore;
using SimpleCowApi.Data.Models;

namespace SimpleCowApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Farm> Farms { get; set; }
        public DbSet<Cow> Cows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding Farms
            //modelBuilder.Entity<Farm>().HasData(
            //    new Farm { Id = 10001, Name = "Sunny Pastures", Location = "Texas" },
            //    new Farm { Id = 10002, Name = "Green Valley Ranch", Location = "Kansas" }
            //);
            //
            //// Seeding Cows
            //modelBuilder.Entity<Cow>().HasData(
            //    new Cow { Id = 20001, Name = "Bessie", Age = 4, FarmId = 10001 },
            //    new Cow { Id = 20002, Name = "MooMoo", Age = 2, FarmId = 10002 },
            //    new Cow { Id = 20003, Name = "Daisy", Age = 3, FarmId = null }  // No farm assigned
            //);
        }
    }
}
