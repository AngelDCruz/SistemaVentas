using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.DTO.Solicitudes.v1
{
    public class CrearRoleDTO
    {

        [
         Required(ErrorMessage = "El role es requerido"),
         MinLength(5, ErrorMessage = "El role debe contener al menos 5 carácteres"),
         MaxLength(20, ErrorMessage = "El role no pude contener más de 20 carácteres")
        ]
        public string Nombre { get; set; }

    }
}
