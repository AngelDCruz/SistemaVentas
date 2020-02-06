using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.DTO.Respuestas.v1
{
    public class RolesDTO
    {

        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string Estatus { get; set; }

    }
}
