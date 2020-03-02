using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.DTO.Solicitudes.v1
{
    public class IncluirUsuariosDTO
    {
        [FromQuery]
        public bool Role { get; set; } = false;
    }
}
