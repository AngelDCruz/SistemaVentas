using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVentas.Infraestructura.Migrations
{
    public partial class detalleventas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    VentaId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ProductoId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    Cantidad = table.Column<int>(type: "INT", nullable: false),
                    Precio = table.Column<decimal>(type: "DECIMAL(4,2)", nullable: false),
                    Descuento = table.Column<decimal>(type: "DECIMAL(4,2)", nullable: false),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: false),
                    VentasId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Ventas_VentasId",
                        column: x => x.VentasId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_ProductoId",
                table: "DetalleVentas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_VentasId",
                table: "DetalleVentas",
                column: "VentasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleVentas");
        }
    }
}
