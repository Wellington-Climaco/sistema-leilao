using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLeilao.Infra.Migrations
{
    /// <inheritdoc />
    public partial class columnIntervaloEntreLances : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "IntervaloEntreLances",
                table: "Leiloes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntervaloEntreLances",
                table: "Leiloes");
        }
    }
}
