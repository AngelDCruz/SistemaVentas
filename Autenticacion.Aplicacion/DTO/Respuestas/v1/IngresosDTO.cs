using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Aplicacion.DTO.Respuestas.v1
{
    public class IngresosDTO
    {

        public Guid Id { get; set; }

        public string TipoComprobante { get; set; }

        public string SerieComprobante { get; set; }

        public double Impuesto { get; set; }

        public double Total { get; set; }

        public string Estatus { get; set; }

    }
}
