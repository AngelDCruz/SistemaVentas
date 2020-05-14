using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Infraestructura.EntidadesConfiguracion
{
    public class VentaConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<VentaEntidad> entidad)
        {

            entidad.Property(x => x.TipoComprobante)
             .HasColumnType("CHAR(10)")
             .IsRequired();

            entidad.Property(x => x.SerieComprobante)
                .HasColumnType("CHAR(12)")
                .IsRequired();

            entidad.Property(x => x.Impuesto)
                .HasColumnType("DECIMAL(4, 2)")
                .IsRequired();

            entidad.Property(x => x.Total)
              .HasColumnType("DECIMAL(11, 2)")
              .IsRequired();

        }

    }
}
