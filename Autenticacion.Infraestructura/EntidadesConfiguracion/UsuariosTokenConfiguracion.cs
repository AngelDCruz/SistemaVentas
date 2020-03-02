
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Dominio.Entidades;

namespace SistemaVentas.Infraestructura.EntidadesConfiguracion
{
    public class UsuariosTokenConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<UsuariosTokenEntidad> entidad)
        {

            entidad.ToTable("UsuariosToken", "dbo");

            //entidad.HasQueryFilter(x => x.Estatus == "Act");

        }

    }
}
