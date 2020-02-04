using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Api.DTO.Respuestas.v1
{
    public class UsuariosRespuestasDTO
    {

        public Guid Id { get; set; }

        public string Usuario { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public string FechaCreacion { get; set; }

        public string Estatus { get; set; }

    }
}
