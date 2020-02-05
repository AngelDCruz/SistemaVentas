using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Api.DTO.Solicitudes.v1
{
    public class CrearUsuarioRolesDTO
    {

        [Required(ErrorMessage = "El id es requerido")]
        public Guid IdUsuario { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        public List<Guid> Roles { get; set; }

    }
}
