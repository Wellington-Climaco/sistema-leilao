using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLeilao.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addColunaIdVencedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrematadoEm",
                table: "Leiloes");

            migrationBuilder.AddColumn<Guid>(
                name: "VencedorId",
                table: "Leiloes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leiloes_VencedorId",
                table: "Leiloes",
                column: "VencedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leiloes_Usuarios_VencedorId",
                table: "Leiloes",
                column: "VencedorId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leiloes_Usuarios_VencedorId",
                table: "Leiloes");

            migrationBuilder.DropIndex(
                name: "IX_Leiloes_VencedorId",
                table: "Leiloes");

            migrationBuilder.DropColumn(
                name: "VencedorId",
                table: "Leiloes");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrematadoEm",
                table: "Leiloes",
                type: "DATETIME",
                nullable: true);
        }
    }
}
