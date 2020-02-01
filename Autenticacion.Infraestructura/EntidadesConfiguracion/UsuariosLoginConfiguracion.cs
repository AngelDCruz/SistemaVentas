using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autenticacion.Infraestructura.EntidadesConfiguracion
{
    public class UsuariosLoginConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<UsuarioLogin> entidad)
        {

            entidad.ToTable("UsuariosLogin", "dbo");

        }

    }
}
