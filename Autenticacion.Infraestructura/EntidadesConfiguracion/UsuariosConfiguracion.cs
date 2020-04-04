using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autenticacion.Infraestructura.EntidadesConfiguracion
{
    public class UsuariosConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<UsuariosEntidad> entidad)
        {

            entidad.ToTable("Usuarios");

            //CAMPOS IGNORADOS
            entidad.Ignore(x => x.PhoneNumberConfirmed);
            entidad.Ignore(x => x.TwoFactorEnabled);
            entidad.Ignore(x => x.LockoutEnd);
            entidad.Ignore(x => x.LockoutEnabled);
            entidad.Ignore(x => x.EmailConfirmed);
            entidad.Ignore(x => x.NormalizedEmail);
            entidad.Ignore(x => x.NormalizedUserName);
            entidad.Ignore(x => x.PhoneNumber);

            entidad.Property(x => x.AccessFailedCount).HasColumnName("IntentosFallidos");
            entidad.Property(x => x.UserName).HasColumnName("Usuario");
            entidad.Property(x => x.PasswordHash).HasColumnName("Password");

            //RELACIONES
            entidad.HasMany(ur => ur.UsuariosReclamaciones)
                .WithOne(u => u.Usuarios)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            entidad.HasMany(ur => ur.UsuariosRoles)
                .WithOne(u => u.Usuarios)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            entidad.HasMany(i => i.Ingresos)
                .WithOne(u => u.Usuarios)
                .HasForeignKey(i => i.UsuariosId);

        }

    }
}
