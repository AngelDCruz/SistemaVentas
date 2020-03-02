using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.DTO.Solicitudes.v1
{
    public class ActualizarUsuarioDTO
    {

        [Required(ErrorMessage = "El id del usuario es requerido")]
        public Guid Id { get; set; }

        [Phone(ErrorMessage = "El número de teléfono no es válido")]
        public string Telefono { get; set; }

    }
}
