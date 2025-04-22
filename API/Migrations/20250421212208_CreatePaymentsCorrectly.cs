using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class CreatePaymentsCorrectly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StripeSessionId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Height = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Width = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Length = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    IdCategory = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdCountry = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdRole = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IdProduct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IdRegion = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Regions_IdRegion",
                        column: x => x.IdRegion,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StreetNameOptional = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdCity = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeliveryAddresses_Cities_IdCity",
                        column: x => x.IdCity,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCustomer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdDeliveryAddress = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPayment = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_IdCustomer",
                        column: x => x.IdCustomer,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryAddresses_IdDeliveryAddress",
                        column: x => x.IdDeliveryAddress,
                        principalTable: "DeliveryAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Payments_IdPayment",
                        column: x => x.IdPayment,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdOrder = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Orders_IdOrder",
                        column: x => x.IdOrder,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Products_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("64a075b8-a5a3-4fee-8896-f7089eedd098"), new DateTime(2025, 4, 21, 21, 22, 3, 791, DateTimeKind.Utc).AddTicks(2452), "Sensores", new DateTime(2025, 4, 21, 21, 22, 3, 791, DateTimeKind.Utc).AddTicks(2453) },
                    { new Guid("a3fd8a0a-0620-4deb-b7d7-e85dc2c866d1"), new DateTime(2025, 4, 21, 21, 22, 3, 791, DateTimeKind.Utc).AddTicks(2446), "Alarmas", new DateTime(2025, 4, 21, 21, 22, 3, 791, DateTimeKind.Utc).AddTicks(2448) },
                    { new Guid("f645755d-c680-4091-9600-5c3e041dc495"), new DateTime(2025, 4, 21, 21, 22, 3, 791, DateTimeKind.Utc).AddTicks(1027), "Cámaras de Seguridad", new DateTime(2025, 4, 21, 21, 22, 3, 791, DateTimeKind.Utc).AddTicks(1036) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Description", "Height", "IdCategory", "Length", "Name", "Price", "Stock", "UpdatedAt", "Weight", "Width" },
                values: new object[,]
                {
                    { new Guid("3468403f-a527-4b56-ab2d-a36bf3fb9c3a"), new DateTime(2025, 4, 21, 21, 22, 3, 800, DateTimeKind.Utc).AddTicks(6229), "Cámara de seguridad de alta definición con visión nocturna y grabación en 1080p. Conectividad Wi-Fi y detección de movimiento.", 10m, new Guid("f645755d-c680-4091-9600-5c3e041dc495"), 20m, "Cámara de Seguridad IP 1080p", 120.99m, 50, new DateTime(2025, 4, 21, 21, 22, 3, 800, DateTimeKind.Utc).AddTicks(6238), 0.5m, 15m },
                    { new Guid("3660b7e1-7ec7-4a3f-a04b-f64e4694c019"), new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3517), "Sensor de movimiento PIR (infrarrojo pasivo) para sistemas de alarma. Detecta movimiento en un rango de hasta 10 metros.", 6m, new Guid("64a075b8-a5a3-4fee-8896-f7089eedd098"), 12m, "Sensor de Movimiento PIR", 45.30m, 0, new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3518), 0.3m, 8m },
                    { new Guid("6b4f67f4-93a2-42d9-a90d-9caa2c88837c"), new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3569), "Cámara de seguridad para exteriores, resistente al agua y con visión nocturna. Se conecta a través de Wi-Fi.", 10m, new Guid("f645755d-c680-4091-9600-5c3e041dc495"), 25m, "Cámara de Seguridad para Exteriores", 180.75m, 30, new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3570), 1.0m, 20m },
                    { new Guid("7aa0cbda-e37f-4978-bee2-234100826adf"), new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3560), "Alarma de seguridad para puertas y ventanas. Ideal para prevenir accesos no autorizados en el hogar o negocio.", 5m, new Guid("a3fd8a0a-0620-4deb-b7d7-e85dc2c866d1"), 15m, "Alarma para Puerta/ ventana", 32.99m, 200, new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3560), 0.5m, 10m },
                    { new Guid("cb155d6f-8a0a-4c81-bf7d-89ba46c417ab"), new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3487), "Sistema de alarma inalámbrico con 4 zonas, ideal para viviendas. Compatible con sensores de puertas y ventanas.", 8m, new Guid("a3fd8a0a-0620-4deb-b7d7-e85dc2c866d1"), 25m, "Alarma Inalámbrica 4 Zonas", 150.50m, 100, new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3493), 1.2m, 20m },
                    { new Guid("f2f0d62f-fefe-4eb0-8910-a81427cb2598"), new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3526), "Cámara dome 4K con visión panorámica y grabación en calidad ultra HD. Resistente a condiciones climáticas extremas.", 12m, new Guid("f645755d-c680-4091-9600-5c3e041dc495"), 18m, "Cámara de Seguridad Dome 4K", 299.99m, 50, new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3527), 0.8m, 15m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_IdRegion",
                table: "Cities",
                column: "IdRegion");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_IdUser",
                table: "Customers",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_CityId",
                table: "DeliveryAddresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddresses_IdCity",
                table: "DeliveryAddresses",
                column: "IdCity");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdCustomer",
                table: "Orders",
                column: "IdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdDeliveryAddress",
                table: "Orders",
                column: "IdDeliveryAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdPayment",
                table: "Orders",
                column: "IdPayment");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_IdProduct",
                table: "ProductImages",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IdCategory",
                table: "Products",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_IdCountry",
                table: "Regions",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_IdOrder",
                table: "ShoppingCarts",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_IdProduct",
                table: "ShoppingCarts",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_OrderId",
                table: "ShoppingCarts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdRole",
                table: "Users",
                column: "IdRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "DeliveryAddresses");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
