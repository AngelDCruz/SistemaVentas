
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Autenticacion.Dominio.Entidades;
using Autenticacion.Infraestructura.EntidadesConfiguracion;
using System;

namespace Autenticacion.Infraestructura
{
    public partial class AutenticationDbContext : IdentityDbContext<Usuarios, Roles, Guid>
    {

        public AutenticationDbContext(DbContextOptions<AutenticationDbContext> options): base(options)
        {

        }

        public AutenticationDbContext()
        {

        }

 
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.HasDefaultSchema("Autenticacion");

            UsuariosConfiguracion.AplicarConfiguracion(builder.Entity<Usuarios>());
            RolesConfiguracion.AplicarConfiguracion(builder.Entity<Roles>());
            UsuariosReclamacionesConfiguracion.AplicarConfiguracion(builder.Entity<UsuariosReclamaciones>());
            UsuariosLoginConfiguracion.AplicarConfiguracion(builder.Entity<UsuarioLogin>());
            UsuariosTokenConfiguracion.AplicarConfiguracion(builder.Entity<UsuariosToken>());
            UsuariosReclamacionesConfiguracion.AplicarConfiguracion(builder.Entity<UsuariosReclamaciones>());
            UsuariosRolesConfiguracion.AplicarConfiguracion(builder.Entity<UsuariosRoles>());

            base.OnModelCreating(builder);

        }
    }
}
