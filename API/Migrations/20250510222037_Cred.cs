using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Cred : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_IdUser",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Customers_IdUser",
                table: "Customers");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3468403f-a527-4b56-ab2d-a36bf3fb9c3a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3660b7e1-7ec7-4a3f-a04b-f64e4694c019"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6b4f67f4-93a2-42d9-a90d-9caa2c88837c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7aa0cbda-e37f-4978-bee2-234100826adf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cb155d6f-8a0a-4c81-bf7d-89ba46c417ab"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f2f0d62f-fefe-4eb0-8910-a81427cb2598"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("64a075b8-a5a3-4fee-8896-f7089eedd098"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a3fd8a0a-0620-4deb-b7d7-e85dc2c866d1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f645755d-c680-4091-9600-5c3e041dc495"));

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "IdCredentials",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGuest",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(255)", maxLength: 255, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(255)", maxLength: 255, nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    TokenCreated = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    IdRole = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credentials_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_IdCredentials",
                table: "Customers",
                column: "IdCredentials");

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_IdRole",
                table: "Credentials",
                column: "IdRole");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Credentials_IdCredentials",
                table: "Customers",
                column: "IdCredentials",
                principalTable: "Credentials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Credentials_IdCredentials",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Customers_IdCredentials",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IdCredentials",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsGuest",
                table: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "IdUser",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdRole = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
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
                columns: new[] { "Id", "CreatedAt", "Description", "Height", "IdCategory", "Length", "Name", "Price", "ProdId", "Stock", "UpdatedAt", "Weight", "Width" },
                values: new object[,]
                {
                    { new Guid("3468403f-a527-4b56-ab2d-a36bf3fb9c3a"), new DateTime(2025, 4, 21, 21, 22, 3, 800, DateTimeKind.Utc).AddTicks(6229), "Cámara de seguridad de alta definición con visión nocturna y grabación en 1080p. Conectividad Wi-Fi y detección de movimiento.", 10m, new Guid("f645755d-c680-4091-9600-5c3e041dc495"), 20m, "Cámara de Seguridad IP 1080p", 120.99m, null, 50, new DateTime(2025, 4, 21, 21, 22, 3, 800, DateTimeKind.Utc).AddTicks(6238), 0.5m, 15m },
                    { new Guid("3660b7e1-7ec7-4a3f-a04b-f64e4694c019"), new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3517), "Sensor de movimiento PIR (infrarrojo pasivo) para sistemas de alarma. Detecta movimiento en un rango de hasta 10 metros.", 6m, new Guid("64a075b8-a5a3-4fee-8896-f7089eedd098"), 12m, "Sensor de Movimiento PIR", 45.30m, null, 0, new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3518), 0.3m, 8m },
                    { new Guid("6b4f67f4-93a2-42d9-a90d-9caa2c88837c"), new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3569), "Cámara de seguridad para exteriores, resistente al agua y con visión nocturna. Se conecta a través de Wi-Fi.", 10m, new Guid("f645755d-c680-4091-9600-5c3e041dc495"), 25m, "Cámara de Seguridad para Exteriores", 180.75m, null, 30, new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3570), 1.0m, 20m },
                    { new Guid("7aa0cbda-e37f-4978-bee2-234100826adf"), new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3560), "Alarma de seguridad para puertas y ventanas. Ideal para prevenir accesos no autorizados en el hogar o negocio.", 5m, new Guid("a3fd8a0a-0620-4deb-b7d7-e85dc2c866d1"), 15m, "Alarma para Puerta/ ventana", 32.99m, null, 200, new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3560), 0.5m, 10m },
                    { new Guid("cb155d6f-8a0a-4c81-bf7d-89ba46c417ab"), new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3487), "Sistema de alarma inalámbrico con 4 zonas, ideal para viviendas. Compatible con sensores de puertas y ventanas.", 8m, new Guid("a3fd8a0a-0620-4deb-b7d7-e85dc2c866d1"), 25m, "Alarma Inalámbrica 4 Zonas", 150.50m, null, 100, new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3493), 1.2m, 20m },
                    { new Guid("f2f0d62f-fefe-4eb0-8910-a81427cb2598"), new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3526), "Cámara dome 4K con visión panorámica y grabación en calidad ultra HD. Resistente a condiciones climáticas extremas.", 12m, new Guid("f645755d-c680-4091-9600-5c3e041dc495"), 18m, "Cámara de Seguridad Dome 4K", 299.99m, null, 50, new DateTime(2025, 4, 21, 21, 22, 3, 801, DateTimeKind.Utc).AddTicks(3527), 0.8m, 15m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_IdUser",
                table: "Customers",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdRole",
                table: "Users",
                column: "IdRole");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_IdUser",
                table: "Customers",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
