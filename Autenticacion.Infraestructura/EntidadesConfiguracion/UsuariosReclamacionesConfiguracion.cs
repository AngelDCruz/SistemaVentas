using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.EntidadesConfiguracion
{
    public class UsuariosReclamacionesConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<UsuariosReclamaciones> entidad)
        {

            entidad.ToTable("UsuariosReclamaciones", "dbo");

        }

    }
}
