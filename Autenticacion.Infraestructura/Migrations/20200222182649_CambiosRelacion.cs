using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Autenticacion.Infraestructura.Migrations
{
    public partial class CambiosRelacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "Autenticacion",
                table: "Usuarios");

            migrationBuilder.AlterColumn<Guid>(
                name: "DatosPersonalesId",
                schema: "Autenticacion",
                table: "Usuarios",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "DatosPersonalesId",
                schema: "Autenticacion",
                table: "Usuarios",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "Autenticacion",
                table: "Usuarios",
                nullable: true);
        }
    }
}
