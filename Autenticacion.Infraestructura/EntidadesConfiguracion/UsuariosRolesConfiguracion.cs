﻿using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Autenticacion.Infraestructura.EntidadesConfiguracion
{
    public class UsuariosRolesConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<UsuariosRolesEntidad> entidad)
        {

            entidad.ToTable("UsuariosRoles");

            //entidad.HasQueryFilter(x => x.Estatus != "Baj");
            entidad.HasOne(r => r.Roles)
                        .WithMany(x => x.UsuariosRoles)
                        .HasForeignKey(r => r.RoleId)
                        .OnDelete(DeleteBehavior.Cascade);

            entidad.HasOne(u => u.Usuarios)
                .WithMany(x => x.UsuariosRoles)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
