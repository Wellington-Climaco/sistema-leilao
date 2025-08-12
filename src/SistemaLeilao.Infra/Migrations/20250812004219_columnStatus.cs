using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLeilao.Infra.Migrations
{
    /// <inheritdoc />
    public partial class columnStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finalizado",
                table: "Leiloes");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Leiloes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Leiloes");

            migrationBuilder.AddColumn<bool>(
                name: "Finalizado",
                table: "Leiloes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
