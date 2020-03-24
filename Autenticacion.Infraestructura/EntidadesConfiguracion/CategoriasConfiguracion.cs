using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Infraestructura.EntidadesConfiguracion
{
    public class CategoriasConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder <CategoriasEntidad> entidad)
        {

            entidad.HasKey(x => x.Id);
            entidad.Property(x => x.Id)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasDefaultValueSql("NEWID()")
                .IsRequired();

            entidad.Property(x => x.Nombre)
                    .HasColumnType("VARCHAR(50)")
                    .IsRequired();

            entidad.Property(x => x.Descripcion)
                .HasColumnType("VARCHAR(100)");

            //RELACIONES
            entidad.HasMany(c => c.Productos)
                 .WithOne(p => p.Categorias)
                 .HasForeignKey(p => p.CategoriaId);


        }

    }
}
