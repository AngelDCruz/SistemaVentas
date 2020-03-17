using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SistemaVentas.Aplicacion.DTO.Solicitudes.v1
{
    public class CrearProductoDTO
    {

        [Required]
        [MaxLength(50, ErrorMessage = "El nombre de la categoría debe de contener máximo 50 caracteres")]
        [MinLength(3, ErrorMessage = "El nombre de la categoría debe de contener minímo 3 caracteres")]
        public string Nombre { get; set; }

        [MaxLength(100, ErrorMessage = "La descripción de la categoría debe de contener máximo 50 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El codigo es requerido")]
        [MaxLength(10, ErrorMessage = "La longitud máxima del código es de 10 caracteres")]
        [MinLength(10, ErrorMessage = "La longitud miníma del código es de 10 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "La categoría es requerida")]
        public Guid CategoriaId { get; set; }

    }
}
