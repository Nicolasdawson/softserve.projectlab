using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.implementations.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);
                entity.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);

                //Defining the Foreign key relationship
                entity.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.IdRole)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Role>(entity => 
            { 
                entity.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<ShoppingCart>()
                .Property(s => s.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
