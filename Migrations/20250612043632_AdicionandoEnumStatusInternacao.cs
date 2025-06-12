using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospisim.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoEnumStatusInternacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusIternacao",
                table: "Internacoes");

            migrationBuilder.AddColumn<int>(
                name: "StatusInternacao",
                table: "Internacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusInternacao",
                table: "Internacoes");

            migrationBuilder.AddColumn<string>(
                name: "StatusIternacao",
                table: "Internacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
