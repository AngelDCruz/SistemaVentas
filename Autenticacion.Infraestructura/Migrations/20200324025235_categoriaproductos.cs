using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVentas.Infraestructura.Migrations
{
    public partial class categoriaproductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_DatosPersonales_DatosPersonalesId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_DatosPersonalesId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "DatosPersonalesId",
                table: "Usuarios");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos");

            migrationBuilder.AddColumn<Guid>(
                name: "DatosPersonalesId",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_DatosPersonalesId",
                table: "Usuarios",
                column: "DatosPersonalesId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_DatosPersonales_DatosPersonalesId",
                table: "Usuarios",
                column: "DatosPersonalesId",
                principalTable: "DatosPersonales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
