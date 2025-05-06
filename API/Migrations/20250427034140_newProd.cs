using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class newProd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProdId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProdId",
                table: "Products",
                column: "ProdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Products_ProdId",
                table: "Products",
                column: "ProdId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Products_ProdId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProdId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProdId",
                table: "Products");
        }
    }
}
