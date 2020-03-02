using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.DTO.Solicitudes.v1
{
    public class ActualizarRoleDTO
    {

        [Required(ErrorMessage = "El id es requerido")]
        public Guid Id { get; set; }

        [
         Required(ErrorMessage = "El role es requerido"),
         MinLength(5, ErrorMessage = "El role debe contener al menos 5 carácteres"),
         MaxLength(20, ErrorMessage = "El role no pude contener más de 20 carácteres")
        ]
        public string Nombre { get; set; }

    }
}
