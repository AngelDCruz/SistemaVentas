using SistemaVentas.Dominio.ModelStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Dominio.Repositorio
{
    public interface IDetallesIngresoRepositorio
    {

        Task<List<FacturaDetalleIngreso>> ObtenerDetallesIngresosIdAsync(Guid idIngreso);

    }
}
