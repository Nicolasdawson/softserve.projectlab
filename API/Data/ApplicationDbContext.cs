using System;
using System.Collections.Generic;
using API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BranchEntity> BranchEntities { get; set; }

    public virtual DbSet<BusinessCustomerEntity> BusinessCustomerEntities { get; set; }

    public virtual DbSet<CartEntity> CartEntities { get; set; }

    public virtual DbSet<CartItemEntity> CartItemEntities { get; set; }

    public virtual DbSet<CatalogCategoryEntity> CatalogCategoryEntities { get; set; }

    public virtual DbSet<CatalogEntity> CatalogEntities { get; set; }

    public virtual DbSet<CategoryEntity> CategoryEntities { get; set; }

    public virtual DbSet<CreditTransactionEntity> CreditTransactionEntities { get; set; }

    public virtual DbSet<CustomerEntity> CustomerEntities { get; set; }

    public virtual DbSet<IndividualCustomerEntity> IndividualCustomerEntities { get; set; }

    public virtual DbSet<ItemEntity> ItemEntities { get; set; }

    public virtual DbSet<LineOfCreditEntity> LineOfCreditEntities { get; set; }

    public virtual DbSet<OrderEntity> OrderEntities { get; set; }

    public virtual DbSet<OrderItemEntity> OrderItemEntities { get; set; }

    public virtual DbSet<PackageEntity> PackageEntities { get; set; }

    public virtual DbSet<PackageItemEntity> PackageItemEntities { get; set; }

    public virtual DbSet<PackageNoteEntity> PackageNoteEntities { get; set; }

    public virtual DbSet<PermissionEntity> PermissionEntities { get; set; }

    public virtual DbSet<PremiumCustomerEntity> PremiumCustomerEntities { get; set; }

    public virtual DbSet<RoleEntity> RoleEntities { get; set; }

    public virtual DbSet<RolePermissionEntity> RolePermissionEntities { get; set; }

    public virtual DbSet<SupplierEntity> SupplierEntities { get; set; }

    public virtual DbSet<SupplierItemEntity> SupplierItemEntities { get; set; }

    public virtual DbSet<UserEntity> UserEntities { get; set; }

    public virtual DbSet<UserRoleEntity> UserRoleEntities { get; set; }

    public virtual DbSet<WarehouseEntity> WarehouseEntities { get; set; }

    public virtual DbSet<WarehouseItemEntity> WarehouseItemEntities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=softserve-chile.database.windows.net;Initial Catalog=RanAwayDB;Persist Security Info=True;User ID=softserve;Password=Admin123;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BranchEntity>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PK__BranchEn__A1682FC55A5F1252");

            entity.ToTable("BranchEntity");

            entity.Property(e => e.BranchAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
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
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<BusinessCustomerEntity>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Business__A4AE64D86704E51B");

            entity.ToTable("BusinessCustomerEntity");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.AnnualRevenue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BusinessSize)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreditTerms)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Industry)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TaxId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VolumeDiscountRate).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Customer).WithOne(p => p.BusinessCustomerEntity)
                .HasForeignKey<BusinessCustomerEntity>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessCustomer_Customer");
        });

        modelBuilder.Entity<CartEntity>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__CartEnti__51BCD7B7FAA86944");

            entity.ToTable("CartEntity");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.CartEntities)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_Customer");
        });

        modelBuilder.Entity<CartItemEntity>(entity =>
        {
            entity.HasKey(e => new { e.CartId, e.Sku }).HasName("PK__CartItem__0D1D2A8BD0863090");

            entity.ToTable("CartItemEntity");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItemEntities)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartItem_Cart");

            entity.HasOne(d => d.SkuNavigation).WithMany(p => p.CartItemEntities)
                .HasForeignKey(d => d.Sku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartItem_Item");
        });

        modelBuilder.Entity<CatalogCategoryEntity>(entity =>
        {
            entity.HasKey(e => new { e.CatalogId, e.CategoryId }).HasName("PK__CatalogC__63C1A8C816684A4C");

            entity.ToTable("CatalogCategoryEntity");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Catalog).WithMany(p => p.CatalogCategoryEntities)
                .HasForeignKey(d => d.CatalogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogCategoryEntity_CatalogEntity");

            entity.HasOne(d => d.Category).WithMany(p => p.CatalogCategoryEntities)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogCategoryEntity_CategoryEntity");
        });

        modelBuilder.Entity<CatalogEntity>(entity =>
        {
            entity.HasKey(e => e.CatalogId).HasName("PK__CatalogE__C2513B68EAF6E389");

            entity.ToTable("CatalogEntity");

            entity.Property(e => e.CatalogDescription).IsUnicode(false);
            entity.Property(e => e.CatalogName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0BA128F819");

            entity.ToTable("CategoryEntity");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<CreditTransactionEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CreditTr__3214EC07E8179EFC");

            entity.ToTable("CreditTransactionEntity");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.LineOfCredit).WithMany(p => p.CreditTransactionEntities)
                .HasForeignKey(d => d.LineOfCreditId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CreditTransaction_LineOfCredit");
        });

        modelBuilder.Entity<CustomerEntity>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8C0587849");

            entity.ToTable("CustomerEntity");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerContactEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CustomerContactNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CustomerType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<IndividualCustomerEntity>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Individu__A4AE64D8DAEDF1F0");

            entity.ToTable("IndividualCustomerEntity");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.CommunicationPreference)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastPurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithOne(p => p.IndividualCustomerEntity)
                .HasForeignKey<IndividualCustomerEntity>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IndividualCustomer_Customer");
        });

        modelBuilder.Entity<ItemEntity>(entity =>
        {
            entity.HasKey(e => e.Sku);

            entity.ToTable("ItemEntity");

            entity.Property(e => e.Sku).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemAdditionalTax).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ItemCurrency)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ItemDescription).IsUnicode(false);
            entity.Property(e => e.ItemDiscount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ItemId).ValueGeneratedOnAdd();
            entity.Property(e => e.ItemImage).IsUnicode(false);
            entity.Property(e => e.ItemMarginGain).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ItemName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ItemPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ItemUnitCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.ItemEntities)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_Category");
        });

        modelBuilder.Entity<LineOfCreditEntity>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__LineOfCr__A4AE64D887773971");

            entity.ToTable("LineOfCreditEntity");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreditLimit).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CurrentBalance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithOne(p => p.LineOfCreditEntity)
                .HasForeignKey<LineOfCreditEntity>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LineOfCredit_Customer");
        });

        modelBuilder.Entity<OrderEntity>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__OrderEnt__C3905BCF818D52E2");

            entity.ToTable("OrderEntity");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderTotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.OrderEntities)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Customer");
        });

        modelBuilder.Entity<OrderItemEntity>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.Sku }).HasName("PK__OrderIte__9F31A6F3E56B5673");

            entity.ToTable("OrderItemEntity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItemEntities)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Order");

            entity.HasOne(d => d.SkuNavigation).WithMany(p => p.OrderItemEntities)
                .HasForeignKey(d => d.Sku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Item");
        });

        modelBuilder.Entity<PackageEntity>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__PackageE__322035CC90C69A78");

            entity.ToTable("PackageEntity");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PackageName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<PackageItemEntity>(entity =>
        {
            entity.HasKey(e => new { e.PackageId, e.Sku }).HasName("PK__PackageI__6E81C8F0B3E005B7");

            entity.ToTable("PackageItemEntity");

            entity.HasOne(d => d.Package).WithMany(p => p.PackageItemEntities)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PackageItem_Package");

            entity.HasOne(d => d.SkuNavigation).WithMany(p => p.PackageItemEntities)
                .HasForeignKey(d => d.Sku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PackageItem_Item");
        });

        modelBuilder.Entity<PackageNoteEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PackageN__3214EC079F039272");

            entity.ToTable("PackageNoteEntity");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.Content).IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Package).WithMany(p => p.PackageNoteEntities)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK_PackageNote_Package");
        });

        modelBuilder.Entity<PermissionEntity>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__Permissi__EFA6FB2F0A8830BD");

            entity.ToTable("PermissionEntity");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PermissionDescription).IsUnicode(false);
            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<PremiumCustomerEntity>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__PremiumC__A4AE64D883286329");

            entity.ToTable("PremiumCustomerEntity");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DiscountRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MembershipExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.MembershipStartDate).HasColumnType("datetime");
            entity.Property(e => e.TierLevel)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithOne(p => p.PremiumCustomerEntity)
                .HasForeignKey<PremiumCustomerEntity>(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PremiumCustomer_Customer");
        });

        modelBuilder.Entity<RoleEntity>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__RoleEnti__8AFACE1A16B28AE0");

            entity.ToTable("RoleEntity");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RoleDescription).IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<RolePermissionEntity>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId }).HasName("PK_RolePermission");

            entity.ToTable("RolePermissionEntity");

            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissionEntities)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermission_Permission");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissionEntities)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePermission_Role");
        });

        modelBuilder.Entity<SupplierEntity>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE666B49DB41DB2");

            entity.ToTable("SupplierEntity");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SupplierAddress).IsUnicode(false);
            entity.Property(e => e.SupplierContactEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SupplierContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SupplierName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<SupplierItemEntity>(entity =>
        {
            entity.HasKey(e => new { e.SupplierId, e.Sku }).HasName("PK__Supplier__17479B88706BD6E0");

            entity.ToTable("SupplierItemEntity");

            entity.HasOne(d => d.SkuNavigation).WithMany(p => p.SupplierItemEntities)
                .HasForeignKey(d => d.Sku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierItem_Item");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplierItemEntities)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierItem_Supplier");
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserEnti__1788CC4CD4265FEF");

            entity.ToTable("UserEntity");

            entity.HasIndex(e => e.UserContactEmail, "UQ__UserEnti__BFDC65101EC93A86").IsUnique();

            entity.HasIndex(e => e.UserContactEmail, "UQ__UserEnti__BFDC651041C3BB3A").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserContactEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserContactNumber)
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

            entity.HasOne(d => d.Branch).WithMany(p => p.UserEntities)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Branch");
        });

        modelBuilder.Entity<UserRoleEntity>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("PK_UserRole");

            entity.ToTable("UserRoleEntity");

            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoleEntities)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoleEntities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_User");
        });

        modelBuilder.Entity<WarehouseEntity>(entity =>
        {
            entity.HasKey(e => e.WarehouseId).HasName("PK__Warehous__2608AFF9FF0E678D");

            entity.ToTable("WarehouseEntity");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.WarehouseLocation)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Branch).WithMany(p => p.WarehouseEntities)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_Branch");
        });

        modelBuilder.Entity<WarehouseItemEntity>(entity =>
        {
            entity.HasKey(e => new { e.WarehouseId, e.Sku }).HasName("PK__Warehous__7AA952C5BCA91125");

            entity.ToTable("WarehouseItemEntity");

            entity.HasOne(d => d.SkuNavigation).WithMany(p => p.WarehouseItemEntities)
                .HasForeignKey(d => d.Sku)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WarehouseItem_Item");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.WarehouseItemEntities)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WarehouseItem_Warehouse");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
