using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaVentas.Aplicacion.DTO.Solicitudes.v1
{
   public  class ActualizarPersonaDTO
    {

        [Required(ErrorMessage = "El id es el requerido")]
        public Guid Id { get; set; }

        [MaxLength(100, ErrorMessage = "El nombre debe de contener máximo 100 caracteres")]
        [MinLength(2, ErrorMessage = "El nombre debe contener minímo 2 caracteres")]
        public string Nombre { get; set; }

        [MaxLength(100, ErrorMessage = "La dirección debe de contener máximo 100 caracteres")]
        [MinLength(2, ErrorMessage = "La dirección debe contener minímo 2 caracteres")]
        public string Direccion { get; set; }

        [Phone(ErrorMessage = "El número de teléfono no es correcto")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "La cuenta de correo electrónico no es válido")]
        public string Email { get; set; }

        public string TipoDocumento { get; set; }

        [MaxLength(18, ErrorMessage = "El número de documento debe de contener máximo 18 caracteres")]
        [MinLength(8, ErrorMessage = "El número de documento debe contener minímo 8 caracteres")]
        public string NumDocumento { get; set; }

    }
}
