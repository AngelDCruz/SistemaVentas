

using System.ComponentModel.DataAnnotations;

namespace Autenticacion.Aplicacion.DTO.Solicitudes.v1
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "El correo electronico es requerido")]
        public string Login { get; set;  }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }

    }
}
