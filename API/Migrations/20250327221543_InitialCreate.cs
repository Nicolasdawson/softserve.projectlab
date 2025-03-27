using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Branch",
            //    columns: table => new
            //    {
            //        BranchId = table.Column<int>(type: "int", nullable: false),
            //        Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
            //        City = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
            //        Region = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
            //        ContactNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
            //        ContactEmail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
            //        Address = table.Column<string>(type: "text", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK__Branch__A1682FC500954FE5", x => x.BranchId);
            //    });

        //    migrationBuilder.CreateTable(
        //        name: "Catalog",
        //        columns: table => new
        //        {
        //            CatalogId = table.Column<int>(type: "int", nullable: false),
        //            Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
        //            Description = table.Column<string>(type: "text", nullable: true),
        //            Status = table.Column<bool>(type: "bit", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Catalog__C2513B6835A0C5A2", x => x.CatalogId);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Category",
        //        columns: table => new
        //        {
        //            CategoryId = table.Column<int>(type: "int", nullable: false),
        //            Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
        //            Status = table.Column<bool>(type: "bit", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Category__19093A0B813139CC", x => x.CategoryId);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Customer",
        //        columns: table => new
        //        {
        //            CustomerId = table.Column<int>(type: "int", nullable: false),
        //            CustomerType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
        //            Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
        //            Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Customer__A4AE64D892601F68", x => x.CustomerId);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Package",
        //        columns: table => new
        //        {
        //            PackageId = table.Column<int>(type: "int", nullable: false),
        //            PackageName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Package__322035CCB13E1AE6", x => x.PackageId);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Permission",
        //        columns: table => new
        //        {
        //            PermissionId = table.Column<int>(type: "int", nullable: false),
        //            PermissionName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
        //            PermissionDescription = table.Column<string>(type: "text", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Permissi__EFA6FB2FC59A3517", x => x.PermissionId);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Role",
        //        columns: table => new
        //        {
        //            RoleId = table.Column<int>(type: "int", nullable: false),
        //            RoleName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
        //            RoleDescription = table.Column<string>(type: "text", nullable: true),
        //            RoleStatus = table.Column<bool>(type: "bit", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Role__8AFACE1A2011D1B1", x => x.RoleId);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Supplier",
        //        columns: table => new
        //        {
        //            SupplierId = table.Column<int>(type: "int", nullable: false),
        //            Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
        //            SupplierAddress = table.Column<string>(type: "text", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Supplier__4BE666B4DF7753F2", x => x.SupplierId);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Users",
        //        columns: table => new
        //        {
        //            UserId = table.Column<int>(type: "int", nullable: false),
        //            UserEmail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
        //            UserFirstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
        //            UserLastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
        //            UserPassword = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
        //            UserStatus = table.Column<bool>(type: "bit", nullable: true),
        //            IsBlocked = table.Column<bool>(type: "bit", nullable: true),
        //            Username = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
        //            BranchId = table.Column<int>(type: "int", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Users__1788CC4CC6DAD6B7", x => x.UserId);
        //            table.ForeignKey(
        //                name: "FK_Users_Branch",
        //                column: x => x.BranchId,
        //                principalTable: "Branch",
        //                principalColumn: "BranchId");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Warehouse",
        //        columns: table => new
        //        {
        //            WarehouseId = table.Column<int>(type: "int", nullable: false),
        //            Location = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
        //            Capacity = table.Column<int>(type: "int", nullable: true),
        //            BranchId = table.Column<int>(type: "int", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Warehous__2608AFF9068F2E75", x => x.WarehouseId);
        //            table.ForeignKey(
        //                name: "FK_Warehouse_Branch",
        //                column: x => x.BranchId,
        //                principalTable: "Branch",
        //                principalColumn: "BranchId");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "CatalogCategory",
        //        columns: table => new
        //        {
        //            CatalogId = table.Column<int>(type: "int", nullable: false),
        //            CategoryId = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.ForeignKey(
        //                name: "FK_CatalogCategory_Catalog",
        //                column: x => x.CatalogId,
        //                principalTable: "Catalog",
        //                principalColumn: "CatalogId");
        //            table.ForeignKey(
        //                name: "FK_CatalogCategory_Category",
        //                column: x => x.CategoryId,
        //                principalTable: "Category",
        //                principalColumn: "CategoryId");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Item",
        //        columns: table => new
        //        {
        //            Sku = table.Column<int>(type: "int", nullable: false),
        //            Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
        //            Description = table.Column<string>(type: "text", nullable: true),
        //            OriginalStock = table.Column<int>(type: "int", nullable: true),
        //            CurrentStock = table.Column<int>(type: "int", nullable: true),
        //            Currency = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
        //            UnitCost = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
        //            MarginGain = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
        //            Discount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
        //            AdditionalTax = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
        //            ItemPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
        //            ItemStatus = table.Column<bool>(type: "bit", nullable: true),
        //            CategoryId = table.Column<int>(type: "int", nullable: true),
        //            Image = table.Column<string>(type: "text", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Item__CA1FD3C4297D061E", x => x.Sku);
        //            table.ForeignKey(
        //                name: "FK_Item_Category",
        //                column: x => x.CategoryId,
        //                principalTable: "Category",
        //                principalColumn: "CategoryId");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Cart",
        //        columns: table => new
        //        {
        //            CartId = table.Column<int>(type: "int", nullable: false),
        //            CustomerId = table.Column<int>(type: "int", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Cart__51BCD7B7103B1C89", x => x.CartId);
        //            table.ForeignKey(
        //                name: "FK_Cart_Customer",
        //                column: x => x.CustomerId,
        //                principalTable: "Customer",
        //                principalColumn: "CustomerId");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "LineOfCredit",
        //        columns: table => new
        //        {
        //            CustomerId = table.Column<int>(type: "int", nullable: false),
        //            CreditLimit = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
        //            CurrentBalance = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__LineOfCr__A4AE64D8236E36B3", x => x.CustomerId);
        //            table.ForeignKey(
        //                name: "FK_LineOfCredit_Customer",
        //                column: x => x.CustomerId,
        //                principalTable: "Customer",
        //                principalColumn: "CustomerId");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Order",
        //        columns: table => new
        //        {
        //            OrderId = table.Column<int>(type: "int", nullable: false),
        //            CustomerId = table.Column<int>(type: "int", nullable: true),
        //            TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
        //            Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
        //            OrderDate = table.Column<DateTime>(type: "datetime", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK__Order__C3905BCF024AFF95", x => x.OrderId);
        //            table.ForeignKey(
        //                name: "FK_Order_Customer",
        //                column: x => x.CustomerId,
        //                principalTable: "Customer",
        //                principalColumn: "CustomerId");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "RolePermission",
        //        columns: table => new
        //        {
        //            RoleId = table.Column<int>(type: "int", nullable: false),
        //            PermissionId = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_RolePermission", x => x.RoleId);
        //            table.ForeignKey(
        //                name: "FK_RolePermission_Permission",
        //                column: x => x.PermissionId,
        //                principalTable: "Permission",
        //                principalColumn: "PermissionId");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "CategoryItem",
        //        columns: table => new
        //        {
        //            CategoryId = table.Column<int>(type: "int", nullable: false),
        //            Sku = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.ForeignKey(
        //                name: "FK_CategoryItem_Category",
        //                column: x => x.CategoryId,
        //                principalTable: "Category",
        //                principalColumn: "CategoryId");
        //            table.ForeignKey(
        //                name: "FK_CategoryItem_Item",
        //                column: x => x.Sku,
        //                principalTable: "Item",
        //                principalColumn: "Sku");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "PackageItem",
        //        columns: table => new
        //        {
        //            PackageId = table.Column<int>(type: "int", nullable: false),
        //            Sku = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.ForeignKey(
        //                name: "FK_PackageItem_Item",
        //                column: x => x.Sku,
        //                principalTable: "Item",
        //                principalColumn: "Sku");
        //            table.ForeignKey(
        //                name: "FK_PackageItem_Package",
        //                column: x => x.PackageId,
        //                principalTable: "Package",
        //                principalColumn: "PackageId");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "SupplierItem",
        //        columns: table => new
        //        {
        //            SupplierId = table.Column<int>(type: "int", nullable: false),
        //            Sku = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.ForeignKey(
        //                name: "FK_SupplierItem_Item",
        //                column: x => x.Sku,
        //                principalTable: "Item",
        //                principalColumn: "Sku");
        //            table.ForeignKey(
        //                name: "FK_SupplierItem_Supplier",
        //                column: x => x.SupplierId,
        //                principalTable: "Supplier",
        //                principalColumn: "SupplierId");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "WarehouseItem",
        //        columns: table => new
        //        {
        //            WarehouseId = table.Column<int>(type: "int", nullable: false),
        //            Sku = table.Column<int>(type: "int", nullable: false),
        //            Stock = table.Column<int>(type: "int", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.ForeignKey(
        //                name: "FK_WarehouseItem_Item",
        //                column: x => x.Sku,
        //                principalTable: "Item",
        //                principalColumn: "Sku");
        //            table.ForeignKey(
        //                name: "FK_WarehouseItem_Warehouse",
        //                column: x => x.WarehouseId,
        //                principalTable: "Warehouse",
        //                principalColumn: "WarehouseId");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "CartItem",
        //        columns: table => new
        //        {
        //            CartId = table.Column<int>(type: "int", nullable: false),
        //            Sku = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.ForeignKey(
        //                name: "FK_CartItem_Cart",
        //                column: x => x.CartId,
        //                principalTable: "Cart",
        //                principalColumn: "CartId");
        //            table.ForeignKey(
        //                name: "FK_CartItem_Item",
        //                column: x => x.Sku,
        //                principalTable: "Item",
        //                principalColumn: "Sku");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "OrderItem",
        //        columns: table => new
        //        {
        //            OrderId = table.Column<int>(type: "int", nullable: false),
        //            Sku = table.Column<int>(type: "int", nullable: false),
        //            Quantity = table.Column<int>(type: "int", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.ForeignKey(
        //                name: "FK_OrderItem_Item",
        //                column: x => x.Sku,
        //                principalTable: "Item",
        //                principalColumn: "Sku");
        //            table.ForeignKey(
        //                name: "FK_OrderItem_Order",
        //                column: x => x.OrderId,
        //                principalTable: "Order",
        //                principalColumn: "OrderId");
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "UserRole",
        //        columns: table => new
        //        {
        //            UserId = table.Column<int>(type: "int", nullable: false),
        //            RoleId = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_UserRole", x => x.UserId);
        //            table.ForeignKey(
        //                name: "FK_UserRole_Role",
        //                column: x => x.RoleId,
        //                principalTable: "Role",
        //                principalColumn: "RoleId");
        //            table.ForeignKey(
        //                name: "FK_UserRole_RolePermission",
        //                column: x => x.RoleId,
        //                principalTable: "RolePermission",
        //                principalColumn: "RoleId");
        //            table.ForeignKey(
        //                name: "FK_UserRole_Users",
        //                column: x => x.UserId,
        //                principalTable: "Users",
        //                principalColumn: "UserId");
        //        });

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Cart_CustomerId",
        //        table: "Cart",
        //        column: "CustomerId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_CartItem_CartId",
        //        table: "CartItem",
        //        column: "CartId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_CartItem_Sku",
        //        table: "CartItem",
        //        column: "Sku");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_CatalogCategory_CatalogId",
        //        table: "CatalogCategory",
        //        column: "CatalogId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_CatalogCategory_CategoryId",
        //        table: "CatalogCategory",
        //        column: "CategoryId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_CategoryItem_CategoryId",
        //        table: "CategoryItem",
        //        column: "CategoryId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_CategoryItem_Sku",
        //        table: "CategoryItem",
        //        column: "Sku");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Item_CategoryId",
        //        table: "Item",
        //        column: "CategoryId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Order_CustomerId",
        //        table: "Order",
        //        column: "CustomerId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_OrderItem_OrderId",
        //        table: "OrderItem",
        //        column: "OrderId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_OrderItem_Sku",
        //        table: "OrderItem",
        //        column: "Sku");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_PackageItem_PackageId",
        //        table: "PackageItem",
        //        column: "PackageId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_PackageItem_Sku",
        //        table: "PackageItem",
        //        column: "Sku");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_RolePermission_PermissionId",
        //        table: "RolePermission",
        //        column: "PermissionId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_SupplierItem_Sku",
        //        table: "SupplierItem",
        //        column: "Sku");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_SupplierItem_SupplierId",
        //        table: "SupplierItem",
        //        column: "SupplierId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_UserRole_RoleId",
        //        table: "UserRole",
        //        column: "RoleId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Users_BranchId",
        //        table: "Users",
        //        column: "BranchId");

        //    migrationBuilder.CreateIndex(
        //        name: "UQ__Users__08638DF888EF9CA9",
        //        table: "Users",
        //        column: "UserEmail",
        //        unique: true,
        //        filter: "[UserEmail] IS NOT NULL");

        //    migrationBuilder.CreateIndex(
        //        name: "UQ__Users__536C85E41539B69F",
        //        table: "Users",
        //        column: "Username",
        //        unique: true,
        //        filter: "[Username] IS NOT NULL");

        //    migrationBuilder.CreateIndex(dotnet ef database update

        //        name: "IX_Warehouse_BranchId",
        //        table: "Warehouse",
        //        column: "BranchId");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_WarehouseItem_Sku",
        //        table: "WarehouseItem",
        //        column: "Sku");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_WarehouseItem_WarehouseId",
        //        table: "WarehouseItem",
        //        column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "CatalogCategory");

            migrationBuilder.DropTable(
                name: "CategoryItem");

            migrationBuilder.DropTable(
                name: "LineOfCredit");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "PackageItem");

            migrationBuilder.DropTable(
                name: "SupplierItem");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "WarehouseItem");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Catalog");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Branch");
        }
    }
}
