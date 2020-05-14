using Autenticacion.Dominio.Entidades;
using Autenticacion.Infraestructura.EntidadesConfiguracion;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Dominio.Entidades;

namespace Autenticacion.Infraestructura
{
    public partial class AppDbContext {

        public DbSet<UsuariosEntidad> Usuarios { get; set; }

        public DbSet<UsuariosReclamacionesEntidad> UsuariosReclamaciones { get; set; }

        public DbSet<RolesEntidad> Roles { get; set; }

        public DbSet<RolesReclamacionesEntidad> RolesReclamaciones { get; set; }

        public DbSet<UsuariosRolesEntidad> UsuariosRoles { get; set; }

        public DbSet<TokenEntidad> Token { get; set; }

        public DbSet<CategoriasEntidad> Categorias { get; set; }

        public DbSet<ProductosEntidad> Productos { get; set; }

        public DbSet<PersonaEntidad> Personas { get; set; }

        public DbSet<IngresoEntidad> Ingresos { get; set; }

        public DbSet<DetalleIngresoEntidad> DetalleIngresos { get; set; }

        public DbSet<VentaEntidad> Ventas { get; set; }

        public DbSet<DetalleVentaEntidad> DetalleVentas { get; set; }

    }
}
