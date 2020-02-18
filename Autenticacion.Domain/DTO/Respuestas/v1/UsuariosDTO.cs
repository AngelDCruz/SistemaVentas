using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.DTO.Respuestas.v1
{
    public class UsuariosDTO
    {

        public Guid Id { get; set; }

        public string Usuario { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string Estatus { get; set; }

        public List<RolesDTO> Roles { get; set; } = new List<RolesDTO>();

    }
}
