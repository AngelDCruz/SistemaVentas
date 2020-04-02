using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Dominio.ModelStore
{
    public class FacturaIngreso
    {

        public Guid Id { get; set; }

        public string Proveedor { get; set; }

        public string Usuario { get; set; }

        public string TipoComprobante { get; set; }

        public string SerieComprobante { get; set; }

        public string Fecha { get; set; }

        public double Total { get; set; }

        public string Estatus { get; set; }

    }
}
