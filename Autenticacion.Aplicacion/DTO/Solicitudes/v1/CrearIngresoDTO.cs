using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Aplicacion.DTO.Solicitudes.v1
{
    public class CrearIngresoDTO
    {

        public Guid PersonaId { get; set; }

        public string TipoComprobante { get; set; }

        public string SerieComprobante { get; set; }

        public double Impuesto { get; set; }

        public double Total { get; set; }

    }
}
