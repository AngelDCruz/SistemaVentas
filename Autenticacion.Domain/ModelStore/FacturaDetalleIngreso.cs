using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Dominio.ModelStore
{
    public class FacturaDetalleIngreso
    {

        public Guid Id { get; set; }

        public string Producto { get; set; }

        public int Cantidad { get; set; }

        public double Precio { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string Estatus { get; set; }

    }
}
