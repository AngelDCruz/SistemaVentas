using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Aplicacion.DTO.Solicitudes.v1
{
    public class CrearDetalleIngresoDTO
    {

        public Guid ProductoId { get; set; }

        public int Cantidad { get; set; }

        public double Precio { get; set; }

    }
}
