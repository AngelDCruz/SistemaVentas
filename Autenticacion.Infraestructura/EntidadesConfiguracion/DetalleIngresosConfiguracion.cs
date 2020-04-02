using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Infraestructura.EntidadesConfiguracion
{
    public class DetalleIngresosConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<DetalleIngresoEntidad> entidad)
        {

            entidad.HasKey(x => x.Id);
            entidad.Property(x => x.Id)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasDefaultValueSql("NEWID()")
                .IsRequired();

            entidad.Property(x => x.Cantidad)
                .HasColumnType("INT")
                .IsRequired();

            entidad.Property(x => x.Precio)
                .HasColumnType("DECIMAL(4,2)")
                .IsRequired();

            entidad.HasOne(i => i.Ingresos)
                .WithMany(d => d.DetalleIngresos);

            entidad.HasOne(p => p.Productos)
                .WithMany(d => d.DetalleIngresos);

        }

    }
}
