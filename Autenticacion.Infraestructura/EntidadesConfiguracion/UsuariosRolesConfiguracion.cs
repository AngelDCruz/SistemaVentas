using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Autenticacion.Infraestructura.EntidadesConfiguracion
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
