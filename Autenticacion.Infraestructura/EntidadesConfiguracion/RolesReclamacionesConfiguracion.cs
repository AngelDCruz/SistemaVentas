
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Dominio.Entidades;

namespace SistemaVentas.Infraestructura.EntidadesConfiguracion
{
    public class RolesReclamacionesConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<RolesReclamacionesEntidad> entidad)
        {

            entidad.ToTable("RolesReclamaciones", "dbo");

            //entidad.HasQueryFilter(x => x.Estatus == "Act");

        }

    }
}
