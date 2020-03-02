
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Dominio.Entidades;

namespace SistemaVentas.Infraestructura.EntidadesConfiguracion
{
    public class UsuariosLoginConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<UsuarioLoginEntidad> entidad)
        {

            entidad.ToTable("UsuariosLogin", "dbo");

            //entidad.HasQueryFilter(x => x.Estatus == "Act");

        }

    }
}
