
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Autenticacion.Dominio.Entidades;
using Autenticacion.Infraestructura.EntidadesConfiguracion;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Autenticacion.Infraestructura
{
    public partial class AutenticacionDbContext : IdentityDbContext<Usuarios, Roles, Guid>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AutenticacionDbContext(
            DbContextOptions<AutenticacionDbContext> options,
            IHttpContextAccessor httpContextAccessor
        ) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AutenticacionDbContext()
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
