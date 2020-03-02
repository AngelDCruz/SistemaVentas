
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Infraestructura.EntidadesConfiguracion
{
    public class UsuariosReclamacionesConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<UsuariosReclamacionesEntidad> entidad)
        {

            entidad.ToTable("UsuariosReclamaciones", "dbo");

            //entidad.HasQueryFilter(x => x.Estatus == "Act");

        }

    }
}
