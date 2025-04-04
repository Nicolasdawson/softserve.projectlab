using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Entities;

public partial class RanAwayDbContext : DbContext
{
    public RanAwayDbContext()
    {
    }

    public RanAwayDbContext(DbContextOptions<RanAwayDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BranchEntity> BranchEntities { get; set; }
    public virtual DbSet<CartEntity> CartEntities { get; set; }
    public virtual DbSet<CartItemEntity> CartItemEntities { get; set; }
    public virtual DbSet<CatalogCategoryEntity> CatalogCategoryEntities { get; set; }
    public virtual DbSet<CatalogEntity> CatalogEntities { get; set; }
    public virtual DbSet<CategoryEntity> CategoryEntities { get; set; }
    public virtual DbSet<CategoryItemEntity> CategoryItemEntities { get; set; }
    public virtual DbSet<CustomerEntity> CustomerEntities { get; set; }
    public virtual DbSet<ItemEntity> ItemEntities { get; set; }
    public virtual DbSet<LineOfCreditEntity> LineOfCreditEntities { get; set; }
    public virtual DbSet<OrderEntity> OrderEntities { get; set; }
    public virtual DbSet<OrderItemEntity> OrderItemEntities { get; set; }
    public virtual DbSet<PackageEntity> PackageEntities { get; set; }
    public virtual DbSet<PackageItemEntity> PackageItemEntities { get; set; }
    public virtual DbSet<PermissionEntity> PermissionEntities { get; set; }
    public virtual DbSet<RoleEntity> RoleEntities { get; set; }
    public virtual DbSet<RolePermissionEntity> RolePermissionEntities { get; set; }
    public virtual DbSet<SupplierEntity> SupplierEntities { get; set; }
    public virtual DbSet<SupplierItemEntity> SupplierItemEntities { get; set; }
    public virtual DbSet<UserRoleEntity> UserRoleEntities { get; set; }
    public virtual DbSet<UsersEntity> UsersEntities { get; set; }
    public virtual DbSet<WarehouseEntity> WarehouseEntities { get; set; }
    public virtual DbSet<WarehouseItemEntity> WarehouseItemEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BranchEntity>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PK__BranchEn__A1682FC5D066FBB7");

            entity.ToTable("BranchEntity");

            entity.Property(e => e.BranchId).ValueGeneratedOnAdd();
            entity.Property(e => e.BranchAddress).HasColumnType("text");
            entity.Property(e => e.BranchCity)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BranchContactEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BranchContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BranchName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BranchRegion)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CartEntity>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__CartEnti__51BCD7B7654A006B");

            entity.ToTable("CartEntity");

            entity.Property(e => e.CartId).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.CartEntities)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Cart_Customer");
        });

        modelBuilder.Entity<CartItemEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CartItemEntity");

            entity.HasOne(d => d.Cart).WithMany()
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartItem_Cart");

            entity.HasOne(d => d.SkuNavigation).WithMany()
                .HasForeignKey(d => d.Sku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartItem_Item");
        });

        modelBuilder.Entity<CatalogCategoryEntity>(entity =>
        {
            entity.HasKey(e => new { e.CatalogId, e.CategoryId }).HasName("PK_CatalogCategory");

            entity.ToTable("CatalogCategoryEntity");

            entity.HasOne(d => d.Catalog).WithMany(p => p.CatalogCategories)
                .HasForeignKey(d => d.CatalogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogCategoryEntity_CatalogEntity");

            entity.HasOne(d => d.Category).WithMany(p => p.CatalogCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogCategoryEntity_CategoryEntity");
        });

        modelBuilder.Entity<CatalogEntity>(entity =>
        {
            entity.HasKey(e => e.CatalogId).HasName("PK__CatalogE__C2513B68A2927962");

            entity.ToTable("CatalogEntity");

            entity.Property(e => e.CatalogId).ValueGeneratedNever();
            entity.Property(e => e.CatalogDescription).HasColumnType("text");
            entity.Property(e => e.CatalogName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B908F146C");

            entity.ToTable("CategoryEntity");

            entity.Property(e => e.CategoryId).ValueGeneratedNever();
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ItemEntity>(entity =>
        {
            entity.HasKey(e => e.Sku).HasName("PK__ItemEnti__CA1FD3C4DE3C2DC2");

            entity.ToTable("ItemEntity");

            entity.Property(e => e.Sku).ValueGeneratedNever();
            entity.Property(e => e.ItemAdditionalTax).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ItemCurrency)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ItemDescription).HasColumnType("text");
            entity.Property(e => e.ItemDiscount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ItemImage).HasColumnType("text");
            entity.Property(e => e.ItemPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ItemMarginGain).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ItemName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ItemUnitCost).HasColumnType("decimal(10, 2)");

            // Define relationship to CategoryItemEntity (Junction Table)
            entity.HasMany(i => i.CategoryItems)
                .WithOne(ci => ci.Item)
                .HasForeignKey(ci => ci.Sku)
                .OnDelete(DeleteBehavior.Cascade);

            // Define relationship to CategoryEntity
            entity.HasOne(i => i.Category)
                .WithMany(c => c.ItemEntities)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_Category");
        });

        modelBuilder.Entity<LineOfCreditEntity>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__LineOfCr__A4AE64D8AD2D6349");

            entity.ToTable("LineOfCreditEntity");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.CreditLimit).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CurrentBalance).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithOne(p => p.LineOfCreditEntity)
                .HasForeignKey<LineOfCreditEntity>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LineOfCredit_Customer");
        });

        modelBuilder.Entity<OrderEntity>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__OrderEnt__C3905BCF953CE3AE");

            entity.ToTable("OrderEntity");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderTotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.OrderEntities)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Order_Customer");
        });

        modelBuilder.Entity<OrderItemEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OrderItemEntity");

            entity.HasOne(d => d.Order).WithMany()
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Order");

            entity.HasOne(d => d.SkuNavigation).WithMany()
                .HasForeignKey(d => d.Sku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Item");
        });

        modelBuilder.Entity<PackageEntity>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__PackageE__322035CC22CDB2BE");

            entity.ToTable("PackageEntity");

            entity.Property(e => e.PackageId).ValueGeneratedNever();
            entity.Property(e => e.PackageName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PackageItemEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PackageItemEntity");

            entity.HasOne(d => d.Package).WithMany()
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PackageItem_Package");

            entity.HasOne(d => d.SkuNavigation).WithMany()
                .HasForeignKey(d => d.Sku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PackageItem_Item");
        });

        modelBuilder.Entity<PermissionEntity>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__Permissi__EFA6FB2FACBAD590");

            entity.ToTable("PermissionEntity");

            entity.Property(e => e.PermissionId).ValueGeneratedNever();
            entity.Property(e => e.PermissionDescription).HasColumnType("text");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoleEntity>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__RoleEnti__8AFACE1A034DFF0F");

            entity.ToTable("RoleEntity");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleDescription).HasColumnType("text");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolePermissionEntity>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_RolePermission");

            entity.ToTable("RolePermissionEntity");

            entity.Property(e => e.RoleId).ValueGeneratedNever();

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissionEntities)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermission_Permission");
        });

        modelBuilder.Entity<SupplierEntity>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE666B42B658447");

            entity.ToTable("SupplierEntity");

            entity.Property(e => e.SupplierId).ValueGeneratedNever();
            entity.Property(e => e.SupplierName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SupplierAddress).HasColumnType("text");
        });

        modelBuilder.Entity<SupplierItemEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SupplierItemEntity");

            entity.HasOne(d => d.SkuNavigation).WithMany()
                .HasForeignKey(d => d.Sku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierItem_Item");

            entity.HasOne(d => d.Supplier).WithMany()
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierItem_Supplier");
        });

        modelBuilder.Entity<UserRoleEntity>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_UserRole");

            entity.ToTable("UserRoleEntity");

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoleEntities)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Role");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.UserRoleEntities)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_RolePermission");

            entity.HasOne(d => d.User).WithOne(p => p.UserRoleEntity)
                .HasForeignKey<UserRoleEntity>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Users");
        });

        modelBuilder.Entity<UsersEntity>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UsersEnt__1788CC4CFA6EDC90");

            entity.ToTable("UsersEntity");

            entity.HasIndex(e => e.UserEmail, "UQ__UsersEnt__08638DF886582A61").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__UsersEnt__536C85E4878D40A5").IsUnique();

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserFirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserLastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Branch).WithMany(p => p.UsersEntities)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK_Users_Branch");
        });

        modelBuilder.Entity<WarehouseEntity>(entity =>
        {
            entity.HasKey(e => e.WarehouseId).HasName("PK__Warehous__2608AFF934015D71");

            entity.ToTable("WarehouseEntity");

            entity.Property(e => e.WarehouseId).ValueGeneratedNever();
            entity.Property(e => e.WarehouseLocation)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Branch).WithMany(p => p.WarehouseEntities)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK_Warehouse_Branch");
        });

        modelBuilder.Entity<WarehouseItemEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("WarehouseItemEntity");

            entity.HasOne(d => d.SkuNavigation).WithMany()
                .HasForeignKey(d => d.Sku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WarehouseItem_Item");

            entity.HasOne(d => d.Warehouse).WithMany()
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WarehouseItem_Warehouse");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
