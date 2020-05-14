using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Infraestructura.EntidadesConfiguracion
{
    public class DetalleVentasConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<DetalleVentaEntidad> entidad)
        {

            entidad.HasKey(x => x.Id);

            entidad.Property(x => x.Id)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasDefaultValueSql("NEWID()")
                .IsRequired();

            entidad.Property(x => x.VentaId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            entidad.Property(x => x.ProductoId)
               .HasColumnType("UNIQUEIDENTIFIER")
               .IsRequired();

            entidad.Property(x => x.Cantidad)
                .HasColumnType("INT")
                .IsRequired();

            entidad.Property(x => x.Precio)
                .HasColumnType("DECIMAL(4,2)")
                .IsRequired();

            entidad.Property(x => x.Descuento)
                .HasColumnType("DECIMAL(4,2)")
                .IsRequired();

            entidad.HasOne(v => v.Ventas)
                .WithMany(dv => dv.DetalleVentas)
                .HasForeignKey(dv => dv.VentaId);

            entidad.HasOne(p => p.Productos)
                .WithMany(dv => dv.DetalleVentas);

        }

    }
}
