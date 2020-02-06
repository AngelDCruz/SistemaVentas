using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Api.DTO.Solicitudes.v1
{
    public class IncluirUsuariosDTO
    {

        [FromQuery]
        public bool Role { get; set; } = false;

    }
}
