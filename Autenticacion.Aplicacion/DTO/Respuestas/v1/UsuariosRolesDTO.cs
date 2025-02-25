﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Aplicacion.DTO.Respuestas.v1
{
    public class UsuariosRolesDTO
    {

        public Guid Id { get; set; }

        public string Usuario { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string Estatus { get; set; }

        public IEnumerable<RolesDTO> Roles { get; set; } = new List<RolesDTO>();

    }
}
