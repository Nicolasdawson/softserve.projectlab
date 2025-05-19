using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class DeleteVersionIdFromCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            // 1. Eliminar relaciones que dependen de 'Id'
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders");

            // 2. Eliminar índices que dependen de 'Id'
            migrationBuilder.DropIndex(
                name: "IX_Customers_Id_IsCurrent",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Id_StartDate_EndDate",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "Id", // si hay un índice llamado simplemente 'Id'
                table: "Customers");

            // 3. Eliminar la clave primaria
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropColumn(
name: "VersionId",
table: "Customers");

            // 4. Eliminar la columna 'Id'
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Customers");

            // 5. Agregar la columna 'Id' como IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // 6. Volver a establecer la clave primaria
            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            // 7. Volver a crear los índices
            migrationBuilder.CreateIndex(
                name: "IX_Customers_Id_IsCurrent",
                table: "Customers",
                columns: new[] { "Id", "IsCurrent" });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Id_StartDate_EndDate",
                table: "Customers",
                columns: new[] { "Id", "StartDate", "EndDate" });

            // 8. Volver a crear la clave foránea
            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade); // o Restrict/SetNull según tu modelo

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

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Customers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "VersionId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_IdCustomer",
                table: "Orders",
                column: "IdCustomer",
                principalTable: "Customers",
                principalColumn: "VersionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
