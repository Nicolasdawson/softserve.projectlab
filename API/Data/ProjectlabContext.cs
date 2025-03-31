using System;
using System.Collections.Generic;
using API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public partial class ProjectlabContext : DbContext
{
    public ProjectlabContext()
    {
    }

    public ProjectlabContext(DbContextOptions<ProjectlabContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Models.Attribute> Attributes { get; set; }

    public virtual DbSet<AttributeCategory> AttributeCategories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserCardPayment> UserCardPayments { get; set; }

    public virtual DbSet<UserPaypalPayment> UserPaypalPayments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Attribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__attribut__3214EC07F7470958");

            entity.ToTable("attribute", "dbo");

            entity.HasIndex(e => e.Field, "RID_Lookup_Field");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.Field).HasMaxLength(255);
            entity.Property(e => e.IdAttributeCategory).HasColumnName("Id_attribute_category");
            entity.Property(e => e.IdProduct).HasColumnName("Id_product");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
            entity.Property(e => e.Value).HasMaxLength(255);

            entity.HasOne(d => d.IdAttributeCategoryNavigation).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.IdAttributeCategory)
                .HasConstraintName("FK__attribute__Id_at__49C3F6B7");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK__attribute__Id_pr__48CFD27E");
        });

        modelBuilder.Entity<AttributeCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__attribut__3214EC07E60DBABE");

            entity.ToTable("attribute_category", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product__3214EC0740F618BD");

            entity.ToTable("product", "dbo");

            entity.HasIndex(e => e.ProductType, "RID_Lookup_Field");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.ProductCategory).HasColumnName("Product_category");
            entity.Property(e => e.ProductType)
                .HasMaxLength(255)
                .HasColumnName("Product_type");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");

            entity.HasOne(d => d.ProductCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategory)
                .HasConstraintName("FK__product__Product__47DBAE45");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product___3214EC07D3917B94");

            entity.ToTable("product_category", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3214EC076EC5F976");

            entity.ToTable("user", "dbo");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.LastUsedPayment).HasColumnName("Last_used_payment");
            entity.Property(e => e.LastUsedPaymentType)
                .HasMaxLength(255)
                .HasColumnName("Last_used_payment_type");
            entity.Property(e => e.Lastname).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
            entity.Property(e => e.User1)
                .HasMaxLength(255)
                .HasColumnName("User");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_add__3214EC0762361649");

            entity.ToTable("user_address", "dbo");

            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .HasColumnName("Address_1");
            entity.Property(e => e.Address2)
                .HasMaxLength(255)
                .HasColumnName("Address_2");
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(255)
                .HasColumnName("Postal_code");
            entity.Property(e => e.State).HasMaxLength(255);
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_addr__User___44FF419A");
        });

        modelBuilder.Entity<UserCardPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_car__3214EC07DE1BEF96");

            entity.ToTable("user_card_payment", "dbo");

            entity.Property(e => e.CardExpirationMonth).HasColumnName("Card_expiration_month");
            entity.Property(e => e.CardExpirationYear).HasColumnName("Card_expiration_year");
            entity.Property(e => e.CardName)
                .HasMaxLength(255)
                .HasColumnName("Card_name");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(255)
                .HasColumnName("Card_number");
            entity.Property(e => e.CardType)
                .HasMaxLength(255)
                .HasColumnName("Card_type");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.LastUsed).HasColumnName("Last_used");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserCardPayments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_card__User___45F365D3");
        });

        modelBuilder.Entity<UserPaypalPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_pay__3214EC0792F1324C");

            entity.ToTable("user_paypal_payment", "dbo");

            entity.Property(e => e.CardExpirationMonth).HasColumnName("Card_expiration_month");
            entity.Property(e => e.CardExpirationYear).HasColumnName("Card_expiration_year");
            entity.Property(e => e.CardName)
                .HasMaxLength(255)
                .HasColumnName("Card_name");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(255)
                .HasColumnName("Card_number");
            entity.Property(e => e.CardType)
                .HasMaxLength(255)
                .HasColumnName("Card_type");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_at");
            entity.Property(e => e.IsActive).HasColumnName("Is_active");
            entity.Property(e => e.LastUsed).HasColumnName("Last_used");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("Update_at");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserPaypalPayments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_payp__User___46E78A0C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
