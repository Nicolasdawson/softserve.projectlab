using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .ToTable("product") // Especifica el nombre exacto de la tabla en la BD
                .HasKey(u => u.Id); // Define la clave primaria
        }

        public DbSet<Category> OurCategory { get; set; }
        public DbSet<Product> Products {  get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Models.Attribute> Attributes {  get; set; }
        public DbSet<AttributeCategory> AttributeCategories { get; set; }

    }
}
