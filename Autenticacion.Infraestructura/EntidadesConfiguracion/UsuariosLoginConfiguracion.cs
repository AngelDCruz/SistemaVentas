using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autenticacion.Infraestructura.EntidadesConfiguracion
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
