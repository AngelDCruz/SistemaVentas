using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Dominio.ModelStore
{
    public class FacturaVenta
    {

        public Guid Id { get; set; }

        public string Cliente { get; set; }

        public string Usuario { get; set; }

        public string TipoComprobante { get; set; }

        public string SerieComprobante { get; set; }

        public decimal Impuesto { get; set; }

        public decimal Total { get; set; }

        public DateTime Fecha { get; set; }

        public string Estatus { get; set; }

    }
}
