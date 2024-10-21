
using MicroServiceMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } // Make sure the DbSet is defined correctly

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set the primary key
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id); // Ensure this matches the property name in the Product model
        }
    }
}
