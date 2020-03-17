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
                name: "DatosPersonales",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: true),
                    ApellidoPaterno = table.Column<string>(maxLength: 50, nullable: true),
                    ApellidoMaterno = table.Column<string>(maxLength: 50, nullable: true),
                    Pais = table.Column<string>(maxLength: 50, nullable: true),
                    Ciudad = table.Column<string>(maxLength: 50, nullable: true),
                    Calle = table.Column<string>(maxLength: 50, nullable: true),
                    Telefono = table.Column<string>(maxLength: 13, nullable: true),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosPersonales", x => x.Id);
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
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
                    Nombre = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Descripcion = table.Column<string>(type: "VARCHAR(100)", nullable: true),
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
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: false),
                    DatosPersonalesId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_DatosPersonales_DatosPersonalesId",
                        column: x => x.DatosPersonalesId,
                        principalTable: "DatosPersonales",
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

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId",
                unique: true);

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
                name: "IX_Usuarios_DatosPersonalesId",
                table: "Usuarios",
                column: "DatosPersonalesId");

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
                name: "Productos");

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
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "DatosPersonales");
        }
    }
}
