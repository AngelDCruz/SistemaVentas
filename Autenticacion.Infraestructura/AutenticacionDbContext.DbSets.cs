using Autenticacion.Dominio.Entidades;
using Autenticacion.Infraestructura.EntidadesConfiguracion;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Infraestructura
{
    public partial class AutenticacionDbContext {

        public DbSet<UsuariosEntidad> Usuarios { get; set; }

        public DbSet<UsuariosReclamacionesEntidad> UsuariosReclamaciones { get; set; }

        public DbSet<RolesEntidad> Roles { get; set; }

        public DbSet<RolesReclamacionesEntidad> RolesReclamaciones { get; set; }

        public DbSet<UsuarioLoginEntidad> UsuarioLogins { get; set; }

        public DbSet<UsuariosRolesEntidad> UsuariosRoles { get; set; }

        public DbSet<UsuariosTokenEntidad> UsuariosTokens { get; set; }

       public DbSet<TokenEntidad> Token { get; set; }

        public DbSet<TokenSessionEntidad> TokenSession { get; set; }

    }
}
