using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Autenticacion.Infraestructura.EntidadesConfiguracion
{
    public class UsuariosRolesConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<UsuariosRoles> entidad)
        {

            entidad.ToTable("UsuariosRoles", "dbo");

        }

    }
}
