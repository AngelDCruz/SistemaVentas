﻿using Autenticacion.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura.EntidadesConfiguracion
{
    public class UsuariosReclamacionesConfiguracion
    {

        public static void AplicarConfiguracion(EntityTypeBuilder<UsuariosReclamacionesEntidad> entidad)
        {

            entidad.ToTable("UsuariosReclamaciones");

            //entidad.HasQueryFilter(x => x.Estatus == "Act");

        }

    }
}
