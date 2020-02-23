using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Aplicacion.DTO.Respuestas.v1
{
    public class AutenticacionDTO
    {

        public string Access_Token { get; set; }

        public string Refresh_Token { get; set; }

        public DateTime Expire { get; set; } = DateTime.UtcNow.ToLocalTime();

        public string Type { get; set; } = "Bearer";

        public bool Valid { get; set; } = false;

        public string Message { get; set; }

    }
}
