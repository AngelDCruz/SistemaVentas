
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
    public partial class AutenticacionDbContext : IdentityDbContext<UsuariosEntidad, RolesEntidad, Guid>
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


            UsuariosConfiguracion.AplicarConfiguracion(builder.Entity<UsuariosEntidad>());
            RolesConfiguracion.AplicarConfiguracion(builder.Entity<RolesEntidad>());
            UsuariosReclamacionesConfiguracion.AplicarConfiguracion(builder.Entity<UsuariosReclamacionesEntidad>());
            UsuariosLoginConfiguracion.AplicarConfiguracion(builder.Entity<UsuarioLoginEntidad>());
            UsuariosTokenConfiguracion.AplicarConfiguracion(builder.Entity<UsuariosTokenEntidad>());
            UsuariosReclamacionesConfiguracion.AplicarConfiguracion(builder.Entity<UsuariosReclamacionesEntidad>());
            UsuariosRolesConfiguracion.AplicarConfiguracion(builder.Entity<UsuariosRolesEntidad>());
            
         

            base.OnModelCreating(builder);

        }
    }
}
