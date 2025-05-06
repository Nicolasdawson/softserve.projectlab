using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API.implementations.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.SeedProducts(); // Para insertar datos
            base.OnModelCreating(modelBuilder);

            //Entity User configuration
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
            
            //Entity Role configuration
            modelBuilder.Entity<Role>(entity => 
            { 
                entity.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
            });

            //Entity Customer configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");

                entity.Property(c => c.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(c => c.User)
                    .WithMany() 
                    .HasForeignKey(c => c.IdUser)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            //Entity Product configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products"); // Table Name in the DB

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("nvarchar(MAX)")
                    .IsUnicode();
                
                entity.Property(P => P.Price)
                    .HasPrecision(18, 2);

                entity.Property(e => e.Weight)
                    .HasPrecision(10, 2);

                entity.Property(e => e.Height)
                    .HasPrecision(10, 2);

                entity.Property(e => e.Width)
                    .HasPrecision(10, 2);

                entity.Property(e => e.Length)
                    .HasPrecision(10, 2);

                entity.Property(e => e.Stock)
                    .IsRequired();
                
                entity.Property(p => p.CreatedAt)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Category)              // Relation with Category
                    .WithMany(c => c.Products)              // One Category has many Products
                    .HasForeignKey(e => e.IdCategory)       // ForeignKey
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.HasMany(e => e.Images)               // Relation with Images
                    .WithOne(pi => pi.Product)              
                    .HasForeignKey(pi => pi.IdProduct)      // ForeignKey
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //Entity Category configuration
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories"); // Table Name in the DB

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(); // `nvarchar` es Unicode en SQL Server
            });

            //Entity ShoppingCart configuration
            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.ToTable("ShoppingCarts");

                entity.Property(sc => sc.Quantity)
                    .IsRequired();

                entity.Property(s => s.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

                entity.HasOne(sc => sc.Product)
                    .WithMany()
                    .HasForeignKey(sc => sc.IdProduct)
                    .OnDelete(DeleteBehavior.Restrict); // Se puede cambiar a 'Cascade' si prefieres la eliminación en cascada

                entity.HasOne(sc => sc.Order)
                    .WithMany()
                    .HasForeignKey(sc => sc.IdOrder)
                    .OnDelete(DeleteBehavior.Restrict); // Se puede cambiar a 'Cascade' si prefieres la eliminación en cascada

            });

            //Entity ProductImage configuration
            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("ProductImages"); // Table Name in the DB

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(); // `nvarchar` es Unicode en SQL Server

                entity.HasOne(e => e.Product)               // Relation with Product
                    .WithMany(p => p.Images)                // One Product has many Images
                    .HasForeignKey(e => e.IdProduct)        // ForeignKey
                    .OnDelete(DeleteBehavior.Cascade)      // Delete Images if the product is deleted
                    .IsRequired(false);
            });

            //Entity Order configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.Property(o => o.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(o => o.Status)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(o => o.TotalPrice)
                    .IsRequired()
                    .HasColumnType("decimal(10,2)");

                // Relaciones
                entity.HasOne(o => o.Customer)
                    .WithMany()
                    .HasForeignKey(o => o.IdCustomer)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasOne(o => o.DeliveryAddress)
                    .WithMany()
                    .HasForeignKey(o => o.IdDeliveryAddress)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.Payment)
                    .WithMany()
                    .HasForeignKey(o => o.IdPayment)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            //Entity Payment configuration
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");

                entity.Property(p => p.TransactionId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.Status)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(p => p.ResponseCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(p => p.WebpayToken)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.PaymentMethod)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(p => p.CardType)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(p => p.CardLastFour)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(p => p.ExpirationDate)
                    .IsRequired()
                    .HasMaxLength(7);

                entity.Property(p => p.Amount)
                    .IsRequired()
                    .HasColumnType("decimal(10,2)");

                entity.Property(p => p.Currency)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(p => p.CardHolderName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            //Entity DeliveryAddress configuration
            modelBuilder.Entity<DeliveryAddress>(entity =>
            {
                entity.ToTable("DeliveryAddresses");

                entity.Property(d => d.StreetName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(d => d.StreetNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(d => d.StreetNameOptional)
                    .HasMaxLength(200);

                // Relación de clave foránea con City
                
                entity.HasOne(d => d.City)
                    .WithMany()
                    .HasForeignKey(d => d.IdCity)
                    .OnDelete(DeleteBehavior.Cascade); // Define la acción de eliminación en cascada                
                
            });

            //Entity City configuration
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("Cities");

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.PostalCode)
                    .IsRequired()
                    .HasMaxLength(20);

                // Relación de clave foránea con Region
                entity.HasOne(c => c.Region) // Una ciudad pertenece a una region
                    .WithMany(r => r.Cities) // Una region tiene muchas ciudades
                    .HasForeignKey(c => c.IdRegion) // Define clave foranea IdRegion
                    .OnDelete(DeleteBehavior.Cascade); // Define la acción de eliminación en cascada
            });

            //Entity Region configuration
            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Regions");

                entity.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                // Relación de clave foránea con Country
                entity.HasOne(r => r.Country)
                    .WithMany(c => c.Regions)
                    .HasForeignKey(r => r.IdCountry)
                    .OnDelete(DeleteBehavior.Cascade); // Define la acción de eliminación en cascada
            });

        }

        public void ClearDatabase()
        {
            Products.RemoveRange(Products);
            Categories.RemoveRange(Categories);
            Countries.RemoveRange(Countries);
            Regions.RemoveRange(Regions);
            SaveChanges();
        }
    }
}
