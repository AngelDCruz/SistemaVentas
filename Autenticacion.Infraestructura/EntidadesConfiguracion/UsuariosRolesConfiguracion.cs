
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Dominio.Entidades;

namespace SistemaVentas.Infraestructura.EntidadesConfiguracion
{
    public class UsuariosRolesConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<UsuariosRolesEntidad> entidad)
        {

            entidad.ToTable("UsuariosRoles", "dbo");

            //entidad.HasQueryFilter(x => x.Estatus != "Baj");

        }

    }
}
