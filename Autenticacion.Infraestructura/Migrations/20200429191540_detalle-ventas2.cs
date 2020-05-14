using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVentas.Infraestructura.Migrations
{
    public partial class detalleventas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleVentas_Ventas_VentasId",
                table: "DetalleVentas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleVentas_VentasId",
                table: "DetalleVentas");

            migrationBuilder.DropColumn(
                name: "VentasId",
                table: "DetalleVentas");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_VentaId",
                table: "DetalleVentas",
                column: "VentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleVentas_Ventas_VentaId",
                table: "DetalleVentas",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleVentas_Ventas_VentaId",
                table: "DetalleVentas");

            migrationBuilder.DropIndex(
                name: "IX_DetalleVentas_VentaId",
                table: "DetalleVentas");

            migrationBuilder.AddColumn<Guid>(
                name: "VentasId",
                table: "DetalleVentas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_VentasId",
                table: "DetalleVentas",
                column: "VentasId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleVentas_Ventas_VentasId",
                table: "DetalleVentas",
                column: "VentasId",
                principalTable: "Ventas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
