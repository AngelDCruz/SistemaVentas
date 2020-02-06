using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.DTO.Solicitudes.v1
{
    public class CrearUsuarioDTO
    {

        [Required(ErrorMessage = "El nombre de usuario es requerido"), 
         MaxLength(20, ErrorMessage = "El nombre de usuario no debe de exceder los 20 caracteres"),
         MinLength(5, ErrorMessage = "El nombre de usuario debe contener al menos 5 caracteres")
        ]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "La cuenta de correo electronico es requerido")]
        [EmailAddress(ErrorMessage = "Ingrese una cuenta de correo electronico valido")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "El número de teléfono no es válido")]
        public string Telefono { get; set; }


    }
}
