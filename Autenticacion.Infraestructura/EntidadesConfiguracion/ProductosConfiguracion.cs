using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SistemaVentas.Dominio.Entidades;

namespace SistemaVentas.Infraestructura.EntidadesConfiguracion
{
    public class ProductosConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<ProductosEntidad> entidad)
        {

            entidad.HasKey(x => x.Id);
            entidad.Property(x => x.Id)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasDefaultValueSql("NEWID()")
                .IsRequired();

            entidad.Property(x => x.Nombre)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

            entidad.Property(x => x.Codigo)
                .HasColumnType("CHAR(10)")
                .IsRequired();

            entidad.Property(x => x.Descripcion)
                .HasColumnType("VARCHAR(100)");

            entidad.Property(x => x.Imagen)
                .HasColumnType("VARCHAR(MAX)");

            entidad.HasMany(d => d.DetalleIngresos)
                .WithOne(p => p.Productos)
                .HasForeignKey(d => d.ProductosId);

            entidad.HasMany(d => d.DetalleVentas)
                .WithOne(p => p.Productos)
                .HasForeignKey(d => d.ProductoId);
      
        }

    }
}
