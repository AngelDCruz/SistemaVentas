using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autenticacion.Infraestructura.EntidadesConfiguracion
{
    public class RolesReclamacionesConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<RolesReclamacionesEntidad> entidad)
        {

            entidad.ToTable("RolesReclamaciones");

            //entidad.HasQueryFilter(x => x.Estatus == "Act");

        }

    }
}
