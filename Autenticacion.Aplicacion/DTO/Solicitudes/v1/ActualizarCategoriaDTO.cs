using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaVentas.Aplicacion.DTO.Solicitudes.v1
{
    public  class ActualizarCategoriaDTO
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El nombre de la categoría debe de contener máximo 50 caracteres")]
        [MinLength(3, ErrorMessage = "El nombre de la categoría debe de contener minímo 3 caracteres")]
        public string Nombre { get; set; }

        [MaxLength(100, ErrorMessage = "La descripción de la categoría debe de contener máximo 50 caracteres")]
        public string Descripcion { get; set; }

    }
}
