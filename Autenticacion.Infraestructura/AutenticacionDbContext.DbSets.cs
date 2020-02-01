using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Infraestructura
{
    public partial class AutenticacionDbContext {

        public DbSet<Usuarios> Usuarios { get; set; }

        public DbSet<UsuariosReclamaciones> UsuariosReclamaciones { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<RolesReclamaciones> RolesReclamaciones { get; set; }

        public DbSet<UsuarioLogin> UsuarioLogins { get; set; }

        public DbSet<UsuariosRoles> UsuariosRoles { get; set; }

        public DbSet<UsuariosToken> UsuariosTokens { get; set; }

    }
}
