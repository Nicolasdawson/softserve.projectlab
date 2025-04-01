using System;
using System.Collections.Generic;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public partial class DbAb6d2eProjectlabContext : DbContext
{
    public DbAb6d2eProjectlabContext()
    {
    }

    public DbAb6d2eProjectlabContext(DbContextOptions<DbAb6d2eProjectlabContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate).HasColumnName("birthDate");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("phone");
            entity.Property(e => e.Wholesale).HasColumnName("wholesale");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.CategoryId, "IX_Product_categoryId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Brand)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("brand");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasForeignKey(d => d.CategoryId);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("ProductCategory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
