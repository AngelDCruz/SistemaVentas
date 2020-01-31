using Autenticacion.Dominio.Repositorio.Contratos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Infraestructura
{
    public class AutenticationDbContext : IdentityDbContext
    {

        public AutenticationDbContext(DbContextOptions<AutenticationDbContext> options): base(options)
        {

        }

    }
}
