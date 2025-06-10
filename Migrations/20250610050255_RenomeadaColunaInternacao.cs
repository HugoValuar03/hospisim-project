using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospisim.Migrations
{
    /// <inheritdoc />
    public partial class RenomeadaColunaInternacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlanoSaudeAtualiziado",
                table: "Internacoes",
                newName: "PlanoSaudeAtualizado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlanoSaudeAtualizado",
                table: "Internacoes",
                newName: "PlanoSaudeAtualiziado");
        }
    }
}
