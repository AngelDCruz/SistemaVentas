using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autenticacion.Infraestructura.EntidadesConfiguracion
{
    public class UsuariosConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<Usuarios> entidad)
        {

            entidad.ToTable("Usuarios");

            //entidad.HasQueryFilter(x => x.Estatus == "Act");

            //RELACIONES
            entidad.HasMany(ur => ur.UsuariosReclamaciones)
                .WithOne(u => u.Usuarios)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            entidad.HasMany(ul => ul.UsuarioLogin)
                .WithOne(u => u.Usuarios)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            entidad.HasMany(ut => ut.UsuariosTokens)
                .WithOne(u => u.Usuarios)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            entidad.HasMany(ur => ur.UsuariosRoles)
                .WithOne(u => u.Usuarios)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

        }

    }
}
