﻿using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autenticacion.Infraestructura.EntidadesConfiguracion
{
    public class RolesReclamacionesConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<RolesReclamaciones> entidad)
        {

            entidad.ToTable("RolesReclamaciones", "dbo");

        }

    }
}
