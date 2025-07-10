 using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLeilao.Infra.Migrations
{
    /// <inheritdoc />
    public partial class updateRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lances_Leiloes_BemId",
                table: "Lances");

            migrationBuilder.RenameColumn(
                name: "BemId",
                table: "Lances",
                newName: "LeilaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Lances_BemId",
                table: "Lances",
                newName: "IX_Lances_LeilaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lances_Leiloes_LeilaoId",
                table: "Lances",
                column: "LeilaoId",
                principalTable: "Leiloes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lances_Leiloes_LeilaoId",
                table: "Lances");

            migrationBuilder.RenameColumn(
                name: "LeilaoId",
                table: "Lances",
                newName: "BemId");

            migrationBuilder.RenameIndex(
                name: "IX_Lances_LeilaoId",
                table: "Lances",
                newName: "IX_Lances_BemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lances_Leiloes_BemId",
                table: "Lances",
                column: "BemId",
                principalTable: "Leiloes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
