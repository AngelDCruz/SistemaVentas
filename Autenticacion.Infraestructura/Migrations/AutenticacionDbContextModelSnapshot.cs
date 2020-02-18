﻿// <auto-generated />
using System;
using Autenticacion.Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Autenticacion.Infraestructura.Migrations
{
    [DbContext(typeof(AutenticacionDbContext))]
    partial class AutenticacionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Autenticacion")
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Autenticacion.Dominio.Entidades.RolesEntidad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Estatus")
                        .IsRequired()
                        .HasColumnType("CHAR(3)");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<Guid>("UsuarioCreacion")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("UsuarioModificacion")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Autenticacion.Dominio.Entidades.TokenSessionEntidad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TokenSession");

                    b.HasKey("Id");

                    b.ToTable("TokenSession");
                });

            modelBuilder.Entity("Autenticacion.Dominio.Entidades.UsuariosEntidad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Estatus")
                        .IsRequired()
                        .HasColumnType("CHAR(3)");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<Guid>("UsuarioCreacion")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("UsuarioModificacion")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Autenticacion.Infraestructura.EntidadesConfiguracion.TokenEntidad", b =>
                {
                    b.Property<string>("Token")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Expiracion");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<string>("JwtId");

                    b.Property<bool>("Usado");

                    b.Property<Guid>("UserId");

                    b.Property<bool>("Valido");

                    b.HasKey("Token");

                    b.HasIndex("UserId");

                    b.ToTable("Token","Autenticacion");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRoleClaim<Guid>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserClaim<Guid>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserLogin<Guid>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<Guid>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserToken<Guid>");
                });

            modelBuilder.Entity("Autenticacion.Dominio.Entidades.RolesReclamacionesEntidad", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>");

                    b.Property<string>("Estatus")
                        .IsRequired()
                        .HasColumnType("CHAR(3)");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<Guid?>("RolesId");

                    b.Property<Guid>("UsuarioCreacion")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("UsuarioModificacion")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.HasIndex("RolesId");

                    b.HasDiscriminator().HasValue("RolesReclamacionesEntidad");
                });

            modelBuilder.Entity("Autenticacion.Dominio.Entidades.UsuariosReclamacionesEntidad", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>");

                    b.Property<string>("Estatus")
                        .IsRequired()
                        .HasColumnType("CHAR(3)");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<Guid>("UsuarioCreacion")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("UsuarioModificacion")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid?>("UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("UsuariosReclamaciones","dbo");

                    b.HasDiscriminator().HasValue("UsuariosReclamacionesEntidad");
                });

            modelBuilder.Entity("Autenticacion.Dominio.Entidades.UsuarioLoginEntidad", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>");

                    b.Property<string>("Estatus")
                        .IsRequired()
                        .HasColumnType("CHAR(3)");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<Guid>("UsuarioCreacion")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("UsuarioModificacion")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid?>("UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("UsuariosLogin","dbo");

                    b.HasDiscriminator().HasValue("UsuarioLoginEntidad");
                });

            modelBuilder.Entity("Autenticacion.Dominio.Entidades.UsuariosRolesEntidad", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>");

                    b.Property<string>("Estatus")
                        .IsRequired()
                        .HasColumnType("CHAR(3)");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<Guid?>("RolesId");

                    b.Property<Guid>("UsuarioCreacion");

                    b.Property<Guid>("UsuarioModificacion")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid?>("UsuariosId");

                    b.HasIndex("RolesId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("UsuariosRoles","dbo");

                    b.HasDiscriminator().HasValue("UsuariosRolesEntidad");
                });

            modelBuilder.Entity("Autenticacion.Dominio.Entidades.UsuariosTokenEntidad", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>");

                    b.Property<string>("Estatus")
                        .IsRequired()
                        .HasColumnType("CHAR(3)");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<Guid>("UsuarioCreacion")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid>("UsuarioModificacion")
                        .HasColumnType("UNIQUEIDENTIFIER");

                    b.Property<Guid?>("UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("UsuariosToken","dbo");

                    b.HasDiscriminator().HasValue("UsuariosTokenEntidad");
                });

            modelBuilder.Entity("Autenticacion.Infraestructura.EntidadesConfiguracion.TokenEntidad", b =>
                {
                    b.HasOne("Autenticacion.Dominio.Entidades.UsuariosEntidad", "Usuarios")
                        .WithMany("Token")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Autenticacion.Dominio.Entidades.RolesEntidad")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Autenticacion.Dominio.Entidades.UsuariosEntidad")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Autenticacion.Dominio.Entidades.UsuariosEntidad")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Autenticacion.Dominio.Entidades.RolesEntidad")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Autenticacion.Dominio.Entidades.UsuariosEntidad")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Autenticacion.Dominio.Entidades.UsuariosEntidad")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Autenticacion.Dominio.Entidades.RolesReclamacionesEntidad", b =>
                {
                    b.HasOne("Autenticacion.Dominio.Entidades.RolesEntidad", "Roles")
                        .WithMany("RoleReclamacion")
                        .HasForeignKey("RolesId");
                });

            modelBuilder.Entity("Autenticacion.Dominio.Entidades.UsuariosReclamacionesEntidad", b =>
                {
                    b.HasOne("Autenticacion.Dominio.Entidades.UsuariosEntidad", "Usuarios")
                        .WithMany("UsuariosReclamaciones")
                        .HasForeignKey("UsuariosId");
                });

            modelBuilder.Entity("Autenticacion.Dominio.Entidades.UsuarioLoginEntidad", b =>
                {
                    b.HasOne("Autenticacion.Dominio.Entidades.UsuariosEntidad", "Usuarios")
                        .WithMany("UsuarioLogin")
                        .HasForeignKey("UsuariosId");
                });

            modelBuilder.Entity("Autenticacion.Dominio.Entidades.UsuariosRolesEntidad", b =>
                {
                    b.HasOne("Autenticacion.Dominio.Entidades.RolesEntidad", "Roles")
                        .WithMany("UsuariosRoles")
                        .HasForeignKey("RolesId");

                    b.HasOne("Autenticacion.Dominio.Entidades.UsuariosEntidad", "Usuarios")
                        .WithMany("UsuariosRoles")
                        .HasForeignKey("UsuariosId");
                });

            modelBuilder.Entity("Autenticacion.Dominio.Entidades.UsuariosTokenEntidad", b =>
                {
                    b.HasOne("Autenticacion.Dominio.Entidades.UsuariosEntidad", "Usuarios")
                        .WithMany("UsuariosTokens")
                        .HasForeignKey("UsuariosId");
                });
#pragma warning restore 612, 618
        }
    }
}
