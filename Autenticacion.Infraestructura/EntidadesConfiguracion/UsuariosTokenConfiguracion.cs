using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autenticacion.Infraestructura.EntidadesConfiguracion
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
