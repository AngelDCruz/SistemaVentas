using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Dominio.ModelStore
{
    public class FacturaDetalleVenta
    {

           public Guid Id { get; set; }

           public string Producto { get; set; }    

            public int Cantidad { get; set; }

            public decimal Precio { get; set; }

            public DateTime Fecha { get; set; }

            public string Estatus { get; set; }

    }
}
