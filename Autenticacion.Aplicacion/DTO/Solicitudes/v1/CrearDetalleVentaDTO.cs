using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaVentas.Aplicacion.DTO.Solicitudes.v1
{
    public class CrearDetalleVentaDTO
    {

        [Required(ErrorMessage = "El producto es requerido")]
        public Guid ProductoId { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio es requerido")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "El descuento es requerido")]
        public double Descuento { get; set; }

    }
}
