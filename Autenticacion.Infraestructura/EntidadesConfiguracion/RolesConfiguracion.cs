
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SistemaVentas.Dominio.Entidades;

namespace SistemaVentas.Infraestructura.EntidadesConfiguracion
{
    public class RolesConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<RolesEntidad> entidad)
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
