
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Aplicacion.DTO.Respuestas.v1
{
    public class UsuariosDTO
    {

        public Guid Id { get; set; }

        public string Usuario { get; set; }

        public string Email { get; set; }

        public string Imagen { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }

        public string Estatus { get; set; }

        public List<RolesDTO> Roles { get; set; }

    }
}
