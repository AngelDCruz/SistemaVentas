using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaVentas.Infraestructura.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Autenticacion");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    Nombre = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Descripcion = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    Nombre = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Direccion = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Telefono = table.Column<string>(type: "VARCHAR(12)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    TipoDocumento = table.Column<string>(type: "VARCHAR(60)", nullable: false),
                    NumDocumento = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    TipoPersona = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Usuario = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    Password = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    IntentosFallidos = table.Column<int>(nullable: false),
                    ImagenPerfil = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    Nombre = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Codigo = table.Column<string>(type: "CHAR(10)", nullable: false),
                    Descripcion = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Imagen = table.Column<string>(type: "VARCHAR(MAX)", nullable: true),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: false),
                    CategoriaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleReclamaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleReclamaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleReclamaciones_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingresos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UsuariosId = table.Column<Guid>(nullable: false),
                    TipoComprobante = table.Column<string>(nullable: true),
                    SerieComprobante = table.Column<string>(nullable: true),
                    Impuesto = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: false),
                    PersonasId = table.Column<Guid>(nullable: false),
                    UsuariosEntidadId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingresos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingresos_Personas_PersonasId",
                        column: x => x.PersonasId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingresos_Usuarios_UsuariosEntidadId",
                        column: x => x.UsuariosEntidadId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioReclamaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioReclamaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioReclamaciones_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<Guid>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                schema: "Autenticacion",
                columns: table => new
                {
                    Token = table.Column<string>(nullable: false),
                    JwtId = table.Column<string>(nullable: true),
                    Usado = table.Column<bool>(nullable: false),
                    Valido = table.Column<bool>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Expiracion = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Token);
                    table.ForeignKey(
                        name: "FK_Token_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleIngresos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IngresosId = table.Column<Guid>(nullable: false),
                    ProductosId = table.Column<Guid>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Precio = table.Column<double>(nullable: false),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleIngresos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleIngresos_Ingresos_IngresosId",
                        column: x => x.IngresosId,
                        principalTable: "Ingresos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleIngresos_Productos_ProductosId",
                        column: x => x.ProductosId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleIngresos_IngresosId",
                table: "DetalleIngresos",
                column: "IngresosId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleIngresos_ProductosId",
                table: "DetalleIngresos",
                column: "ProductosId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_PersonasId",
                table: "Ingresos",
                column: "PersonasId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_UsuariosEntidadId",
                table: "Ingresos",
                column: "UsuariosEntidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleReclamaciones_RoleId",
                table: "RoleReclamaciones",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioReclamaciones_UserId",
                table: "UsuarioReclamaciones",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_RoleId",
                table: "UsuariosRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                schema: "Autenticacion",
                table: "Token",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleIngresos");

            migrationBuilder.DropTable(
                name: "RoleReclamaciones");

            migrationBuilder.DropTable(
                name: "UsuarioReclamaciones");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "Token",
                schema: "Autenticacion");

            migrationBuilder.DropTable(
                name: "Ingresos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
