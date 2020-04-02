using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaVentas.Aplicacion.DTO.Solicitudes.v1
{
    public class CrearPersonaDTO
    {

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre debe de contener máximo 100 caracteres")]
        [MinLength(2, ErrorMessage = "El nombre debe contener minímo 2 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La direccion es correcta")]
        [MaxLength(100, ErrorMessage = "La dirección debe de contener máximo 100 caracteres")]
        [MinLength(2, ErrorMessage = "La dirección debe contener minímo 2 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El número de teléfono es requerido")]
        [Phone(ErrorMessage = "El número de teléfono no es correcto")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "La cuenta de correo electrónico no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El tipo de identificador es requerido")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "El número de clave o identificador es requerido")]
        [MaxLength(18, ErrorMessage = "El número de documento debe de contener máximo 18 caracteres")]
        [MinLength(8, ErrorMessage = "El número de documento debe contener minímo 8 caracteres")]
        public string NumDocumento { get; set; }

    }
}
