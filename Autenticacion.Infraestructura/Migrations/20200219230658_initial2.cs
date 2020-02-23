using Microsoft.EntityFrameworkCore.Migrations;

namespace Autenticacion.Infraestructura.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                schema: "Autenticacion",
                table: "Usuarios",
                newName: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "Autenticacion",
                table: "Usuarios",
                newName: "PasswordHash");
        }
    }
}
