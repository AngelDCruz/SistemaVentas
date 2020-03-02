
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

using SistemaVentas.Infraestructura.EntidadesConfiguracion;
using SistemaVentas.Dominio.Entidades;

namespace SistemaVentas.Infraestructura
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

            builder.HasDefaultSchema("SistemaVentas");


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
