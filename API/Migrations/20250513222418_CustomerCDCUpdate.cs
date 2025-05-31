using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class CustomerCDCUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Remove existing FK and index
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders");
            migrationBuilder.DropIndex(
                name: "IX_Orders_IdCustomer",
                table: "Orders");

            // 2) Drop the old GUID column on Orders
            migrationBuilder.DropColumn(
                name: "IdCustomer",
                table: "Orders");
            /*
            migrationBuilder.DropColumn(
                name: "ProdId",
                table: "Products");
             */
            // 3) Drop primary key on Customers (GUID Id) before changing it
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            // 4) Drop CDC columns on Customers
            migrationBuilder.DropColumn(name: "CreatedAt", table: "Customers");
            migrationBuilder.DropColumn(name: "UpdatedAt", table: "Customers");

            // 5) Change Customers.Id from GUID to int by dropping and re-adding
            migrationBuilder.DropColumn(name: "Id", table: "Customers");
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.CreateIndex(
                name: "Id",
                table: "Customers",
                column: "Id",
                unique: true);
            // 6) Add your CDC/versioning columns
            migrationBuilder.AddColumn<int>(
                name: "VersionId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Customers",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: DateTime.UtcNow);
            migrationBuilder.AddColumn<DateTime?>(
                name: "EndDate",
                table: "Customers",
                type: "datetime2(7)",
                nullable: true);
            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: true);

            // 7) Rebuild keys, indexes, and FKs
            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Id_IsCurrent",
                table: "Customers",
                columns: new[] { "Id", "IsCurrent" });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Id_StartDate_EndDate",
                table: "Customers",
                columns: new[] { "Id", "StartDate", "EndDate" });

            migrationBuilder.AddColumn<int>(
                name: "IdCustomer",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdCustomer",
                table: "Orders",
                column: "IdCustomer");
            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Id_IsCurrent",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Id_StartDate_EndDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "VersionId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Customers");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdCustomer",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
