using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autenticacion.Infraestructura.EntidadesConfiguracion
{
    public class RolesConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<Roles> entidad)
        {

            entidad.ToTable("Roles");

            //entidad.HasQueryFilter(x => x.Estatus == "Act");

            //RELACIONES
            entidad.HasMany(ur => ur.UsuariosRoles)
                   .WithOne(r => r.Roles)
                   .HasForeignKey(r => r.RoleId)
                   .IsRequired();

            entidad.HasMany(rr => rr.RoleReclamacion)
                    .WithOne(r => r.Roles)
                    .HasForeignKey(r => r.RoleId)
                    .IsRequired();

        }

    }
}
