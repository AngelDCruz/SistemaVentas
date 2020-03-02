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
                name: "SistemaVentas");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "SistemaVentas",
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
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "SistemaVentas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "SistemaVentas",
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
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: true),
                    RolesId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "SistemaVentas",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalSchema: "SistemaVentas",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "SistemaVentas",
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
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: true),
                    UsuariosId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "SistemaVentas",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UsuariosId",
                        column: x => x.UsuariosId,
                        principalSchema: "SistemaVentas",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "SistemaVentas",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: true),
                    UsuariosId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "SistemaVentas",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UsuariosId",
                        column: x => x.UsuariosId,
                        principalSchema: "SistemaVentas",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "SistemaVentas",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    UsuariosId = table.Column<Guid>(nullable: true),
                    RolesId = table.Column<Guid>(nullable: true),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "SistemaVentas",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "SistemaVentas",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalSchema: "SistemaVentas",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UsuariosId",
                        column: x => x.UsuariosId,
                        principalSchema: "SistemaVentas",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "SistemaVentas",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    UsuarioCreacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    UsuarioModificacion = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    Estatus = table.Column<string>(type: "CHAR(3)", nullable: true),
                    UsuariosId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "SistemaVentas",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UsuariosId",
                        column: x => x.UsuariosId,
                        principalSchema: "SistemaVentas",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "SistemaVentas",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RolesId",
                schema: "SistemaVentas",
                table: "AspNetRoleClaims",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "SistemaVentas",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "SistemaVentas",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UsuariosId",
                schema: "SistemaVentas",
                table: "AspNetUserClaims",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "SistemaVentas",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UsuariosId",
                schema: "SistemaVentas",
                table: "AspNetUserLogins",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "SistemaVentas",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RolesId",
                schema: "SistemaVentas",
                table: "AspNetUserRoles",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UsuariosId",
                schema: "SistemaVentas",
                table: "AspNetUserRoles",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "SistemaVentas",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "SistemaVentas",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_UsuariosId",
                schema: "SistemaVentas",
                table: "AspNetUserTokens",
                column: "UsuariosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "SistemaVentas");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "SistemaVentas");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "SistemaVentas");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "SistemaVentas");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "SistemaVentas");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "SistemaVentas");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "SistemaVentas");
        }
    }
}
