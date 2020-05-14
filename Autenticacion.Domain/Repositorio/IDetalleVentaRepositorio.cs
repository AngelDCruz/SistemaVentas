using SistemaVentas.Dominio.ModelStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Dominio.Repositorio
{
    public interface IDetalleVentaRepositorio
    {

        Task<List<FacturaDetalleVenta>> ObtenerDetallesVentaPorIdAsync(Guid idVenta);

    }
}
