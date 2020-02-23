using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Aplicacion.DTO.Solicitudes.v1
{
    public class ActualizarDatosPersonalesDTO
    {

        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El nombre no debe de exceder los 50 carácteres")]
        [MinLength(2, ErrorMessage = "El nombre minímo debe de contener 2 carácteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido paterno es requerido")]
        [MaxLength(50, ErrorMessage = "El apellido paterno no debe de exceder los 50 carácteres")]
        [MinLength(2, ErrorMessage = "El apellido paterno minímo debe de contener 2 carácteres")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "El apellido materno es requerido")]
        [MaxLength(50, ErrorMessage = "El apellido materno no debe de exceder los 50 carácteres")]
        [MinLength(2, ErrorMessage = "El apellido materno minímo debe de contener 2 carácteres")]
        public string ApellidoMaterno { get; set; }

        [MaxLength(50, ErrorMessage = "El pais no debe de exceder los 50 carácteres")]
        [MinLength(2, ErrorMessage = "El pais minímo debe de contener 2 carácteres")]
        public string Pais { get; set; }

        [MaxLength(50, ErrorMessage = "El ciudad no debe de exceder los 50 carácteres")]
        [MinLength(2, ErrorMessage = "El ciudad minímo debe de contener 2 carácteres")]
        public string Ciudad { get; set; }

        [MaxLength(50, ErrorMessage = "El calle no debe de exceder los 50 carácteres")]
        [MinLength(2, ErrorMessage = "El calle minímo debe de contener 2 carácteres")]
        public string Calle { get; set; }

        [Phone(ErrorMessage = "El número de teléfono no es valido")]
        public string Telefono { get; set; }

    }
}
