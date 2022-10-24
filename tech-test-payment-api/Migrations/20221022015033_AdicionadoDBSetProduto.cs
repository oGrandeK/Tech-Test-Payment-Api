using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tech_test_payment_api.Migrations
{
    public partial class AdicionadoDBSetProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Venda_VendaId",
                table: "Produto");

            migrationBuilder.AlterColumn<int>(
                name: "VendaId",
                table: "Produto",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Venda_VendaId",
                table: "Produto",
                column: "VendaId",
                principalTable: "Venda",
                principalColumn: "VendaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Venda_VendaId",
                table: "Produto");

            migrationBuilder.AlterColumn<int>(
                name: "VendaId",
                table: "Produto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Venda_VendaId",
                table: "Produto",
                column: "VendaId",
                principalTable: "Venda",
                principalColumn: "VendaId");
        }
    }
}
