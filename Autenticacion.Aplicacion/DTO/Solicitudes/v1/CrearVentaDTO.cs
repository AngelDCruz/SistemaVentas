using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaVentas.Aplicacion.DTO.Solicitudes.v1
{
    public class CrearVentaDTO
    {

        [Required(ErrorMessage = "El cliente es requerido")]
        public Guid PersonaId { get; set; }

        [Required(ErrorMessage = "El tipo de comprobante es requerido")]
        [MaxLength(10, ErrorMessage = "El tipo de comprobante solo puede contener 10 caracteres máximo")]
        public string TipoComprobante { get; set; }

        [Required(ErrorMessage = "La número o serie de comprobante es requerido")]
        [MaxLength(10, ErrorMessage = "El número o serie de comprobante solo puede contener 10 caracteres máximo")]
        public string SerieComprobante { get; set; }

        [Required(ErrorMessage = "El impuesto es requerido")]
        public double Impuesto { get; set; }

        [Required(ErrorMessage = "El total es requerido")]
        public double Total { get; set; }
        
        public List<CrearDetalleVentaDTO> DetalleVentas { get; set; }

    }
}
