using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCustomerIdFromCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Customers_CustomerId",
                table: "Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_CustomerId",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Credentials");

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_IdCustomer",
                table: "Credentials",
                column: "IdCustomer");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Customers_IdCustomer",
                table: "Credentials",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Customers_IdCustomer",
                table: "Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_IdCustomer",
                table: "Credentials");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Credentials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_CustomerId",
                table: "Credentials",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Customers_CustomerId",
                table: "Credentials",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
