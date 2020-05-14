
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Autenticacion.Dominio.Entidades;
using Autenticacion.Infraestructura.EntidadesConfiguracion;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SistemaVentas.Infraestructura.EntidadesConfiguracion;
using SistemaVentas.Dominio.Entidades;

namespace Autenticacion.Infraestructura
{
    public partial class AppDbContext : IdentityDbContext<UsuariosEntidad, RolesEntidad, Guid>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IHttpContextAccessor httpContextAccessor
        ) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AppDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);

            // builder.HasDefaultSchema("Autenticacion");

            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();

             builder.Entity<IdentityUserRole<Guid>>().ToTable("UsuariosRoles");
             builder.Entity<IdentityUserClaim<Guid>>().ToTable("UsuarioReclamaciones");
             builder.Entity<UsuariosRolesEntidad>().ToTable("UsuariosReclamaciones");
             builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleReclamaciones");
    
            UsuariosConfiguracion.AplicarConfiguracion(builder.Entity<UsuariosEntidad>());
            RolesConfiguracion.AplicarConfiguracion(builder.Entity<RolesEntidad>());
            RolesReclamacionesConfiguracion.AplicarConfiguracion(builder.Entity<RolesReclamacionesEntidad>());
            UsuariosReclamacionesConfiguracion.AplicarConfiguracion(builder.Entity<UsuariosReclamacionesEntidad>());
            UsuariosRolesConfiguracion.AplicarConfiguracion(builder.Entity<UsuariosRolesEntidad>());

            CategoriasConfiguracion.AplicarConfiguracion(builder.Entity<CategoriasEntidad>());
            ProductosConfiguracion.AplicarConfiguracion(builder.Entity<ProductosEntidad>());
            PersonaConfiguracion.AplicarConfiguracion(builder.Entity<PersonaEntidad>());
            IngresosConfiguracion.AplicarConfiguracion(builder.Entity<IngresoEntidad>());
            DetalleIngresosConfiguracion.AplicarConfiguracion(builder.Entity<DetalleIngresoEntidad>());
            VentaConfiguracion.AplicarConfiguracion(builder.Entity<VentaEntidad>());
            DetalleVentasConfiguracion.AplicarConfiguracion(builder.Entity<DetalleVentaEntidad>());

        }
    }
}
