using Microsoft.EntityFrameworkCore.Migrations;

namespace Autenticacion.Infraestructura.Migrations
{
    public partial class tokensession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                schema: "Autenticacion",
                table: "TokenSession",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "Autenticacion",
                table: "TokenSession",
                newName: "IdUsuario");
        }
    }
}
