using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Dominio.Entidades;


namespace SistemaVentas.Infraestructura.EntidadesConfiguracion
{
    public class IngresosConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<IngresoEntidad> entidad)
        {

            entidad.HasKey(x => x.Id);
            entidad.Property(x => x.Id)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasDefaultValueSql("NEWID()")
                .IsRequired();

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
