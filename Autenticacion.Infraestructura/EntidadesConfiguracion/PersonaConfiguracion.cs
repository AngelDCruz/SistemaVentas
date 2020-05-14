using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Infraestructura.EntidadesConfiguracion
{
    public class PersonaConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<PersonaEntidad> entidad)
        {

            entidad.HasKey(x => x.Id);

            entidad.Property(x => x.Id)
                   .HasColumnType("UNIQUEIDENTIFIER")
                   .HasDefaultValueSql("NEWID()")
                   .IsRequired();

            entidad.Property(x => x.Nombre)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            entidad.Property(x => x.Direccion)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            entidad.Property(x => x.Telefono)
                .HasColumnType("VARCHAR(12)")
                .IsRequired();

            entidad.Property(x => x.Email)
                .HasColumnType("VARCHAR(60)")
                .IsRequired();

            entidad.Property(x => x.TipoDocumento)
                .HasColumnType("VARCHAR(60)")
                .IsRequired();

            entidad.Property(x => x.NumDocumento)
                .HasColumnType("VARCHAR(20)")
                .IsRequired();

            entidad.Property(x => x.TipoPersona)
               .HasColumnType("VARCHAR(50)")
               .IsRequired();

            entidad.HasMany(i => i.Ingresos)
                .WithOne(p => p.Personas)
                .HasForeignKey(i => i.PersonasId);

            entidad.HasMany(v => v.Ventas)
                .WithOne(p => p.Personas)
                .HasForeignKey(v => v.PersonaId);

        }

    }
}
