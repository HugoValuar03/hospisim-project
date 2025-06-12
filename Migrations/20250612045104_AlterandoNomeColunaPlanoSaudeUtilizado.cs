using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospisim.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoNomeColunaPlanoSaudeUtilizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlanoSaudeAtualizado",
                table: "Internacoes",
                newName: "PlanoSaudeUtilizado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlanoSaudeUtilizado",
                table: "Internacoes",
                newName: "PlanoSaudeAtualizado");
        }
    }
}
