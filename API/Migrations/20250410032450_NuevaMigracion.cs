using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class NuevaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebpayToken",
                table: "Payments");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("7bc32edc-f075-4a37-9fd0-e4a5b107d368"), new DateTime(2025, 4, 10, 3, 24, 46, 435, DateTimeKind.Utc).AddTicks(755), "Alarmas", new DateTime(2025, 4, 10, 3, 24, 46, 435, DateTimeKind.Utc).AddTicks(756) },
                    { new Guid("c850356c-72e5-48e4-b4c4-4b8f4ea30001"), new DateTime(2025, 4, 10, 3, 24, 46, 434, DateTimeKind.Utc).AddTicks(9578), "Cámaras de Seguridad", new DateTime(2025, 4, 10, 3, 24, 46, 434, DateTimeKind.Utc).AddTicks(9585) },
                    { new Guid("daedbd41-43c5-4729-8fbd-ba6e4b3e2ecc"), new DateTime(2025, 4, 10, 3, 24, 46, 435, DateTimeKind.Utc).AddTicks(760), "Sensores", new DateTime(2025, 4, 10, 3, 24, 46, 435, DateTimeKind.Utc).AddTicks(761) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "Description", "Height", "IdCategory", "Length", "Name", "Price", "Stock", "UpdatedAt", "Weight", "Width" },
                values: new object[,]
                {
                    { new Guid("33577b56-b564-4c23-a062-c666b0c220ff"), new DateTime(2025, 4, 10, 3, 24, 46, 437, DateTimeKind.Utc).AddTicks(1078), "Sistema de alarma inalámbrico con 4 zonas, ideal para viviendas. Compatible con sensores de puertas y ventanas.", 8m, new Guid("7bc32edc-f075-4a37-9fd0-e4a5b107d368"), 25m, "Alarma Inalámbrica 4 Zonas", 150.50m, 100, new DateTime(2025, 4, 10, 3, 24, 46, 437, DateTimeKind.Utc).AddTicks(1079), 1.2m, 20m },
                    { new Guid("77d09223-94d7-4511-9bf2-67084b602560"), new DateTime(2025, 4, 10, 3, 24, 46, 437, DateTimeKind.Utc).AddTicks(1144), "Alarma de seguridad para puertas y ventanas. Ideal para prevenir accesos no autorizados en el hogar o negocio.", 5m, new Guid("7bc32edc-f075-4a37-9fd0-e4a5b107d368"), 15m, "Alarma para Puerta/ ventana", 32.99m, 200, new DateTime(2025, 4, 10, 3, 24, 46, 437, DateTimeKind.Utc).AddTicks(1145), 0.5m, 10m },
                    { new Guid("819c8ae2-a95b-4f1c-a376-dc733db0a9ce"), new DateTime(2025, 4, 10, 3, 24, 46, 437, DateTimeKind.Utc).AddTicks(1126), "Sensor de movimiento PIR (infrarrojo pasivo) para sistemas de alarma. Detecta movimiento en un rango de hasta 10 metros.", 6m, new Guid("daedbd41-43c5-4729-8fbd-ba6e4b3e2ecc"), 12m, "Sensor de Movimiento PIR", 45.30m, 0, new DateTime(2025, 4, 10, 3, 24, 46, 437, DateTimeKind.Utc).AddTicks(1127), 0.3m, 8m },
                    { new Guid("b10610b5-8606-4b4c-a5be-81dc761bc9d8"), new DateTime(2025, 4, 10, 3, 24, 46, 437, DateTimeKind.Utc).AddTicks(1153), "Cámara de seguridad para exteriores, resistente al agua y con visión nocturna. Se conecta a través de Wi-Fi.", 10m, new Guid("c850356c-72e5-48e4-b4c4-4b8f4ea30001"), 25m, "Cámara de Seguridad para Exteriores", 180.75m, 30, new DateTime(2025, 4, 10, 3, 24, 46, 437, DateTimeKind.Utc).AddTicks(1153), 1.0m, 20m },
                    { new Guid("d2fe7d0c-d0f7-4539-a57b-3f1697b77118"), new DateTime(2025, 4, 10, 3, 24, 46, 436, DateTimeKind.Utc).AddTicks(6261), "Cámara de seguridad de alta definición con visión nocturna y grabación en 1080p. Conectividad Wi-Fi y detección de movimiento.", 10m, new Guid("c850356c-72e5-48e4-b4c4-4b8f4ea30001"), 20m, "Cámara de Seguridad IP 1080p", 120.99m, 50, new DateTime(2025, 4, 10, 3, 24, 46, 436, DateTimeKind.Utc).AddTicks(6268), 0.5m, 15m },
                    { new Guid("e9b2a087-db87-4a92-9b9f-074cdc0430f0"), new DateTime(2025, 4, 10, 3, 24, 46, 437, DateTimeKind.Utc).AddTicks(1135), "Cámara dome 4K con visión panorámica y grabación en calidad ultra HD. Resistente a condiciones climáticas extremas.", 12m, new Guid("c850356c-72e5-48e4-b4c4-4b8f4ea30001"), 18m, "Cámara de Seguridad Dome 4K", 299.99m, 50, new DateTime(2025, 4, 10, 3, 24, 46, 437, DateTimeKind.Utc).AddTicks(1136), 0.8m, 15m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33577b56-b564-4c23-a062-c666b0c220ff"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("77d09223-94d7-4511-9bf2-67084b602560"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("819c8ae2-a95b-4f1c-a376-dc733db0a9ce"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b10610b5-8606-4b4c-a5be-81dc761bc9d8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d2fe7d0c-d0f7-4539-a57b-3f1697b77118"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e9b2a087-db87-4a92-9b9f-074cdc0430f0"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7bc32edc-f075-4a37-9fd0-e4a5b107d368"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c850356c-72e5-48e4-b4c4-4b8f4ea30001"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("daedbd41-43c5-4729-8fbd-ba6e4b3e2ecc"));

            migrationBuilder.AddColumn<string>(
                name: "WebpayToken",
                table: "Payments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
