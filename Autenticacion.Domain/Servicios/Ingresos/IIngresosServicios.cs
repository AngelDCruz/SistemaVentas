using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.ModelStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Dominio.Servicios.Ingresos
{
    public interface IIngresosServicios
    {

        Task<List<FacturaIngreso>> ObtenerIngresosAsync();

        Task<FacturaIngreso> ObtenerIngresoIdAsync(Guid Id);

        Task<bool> EliminarIngresoAsync(Guid id);

        Task<IngresoEntidad> CrearIngresoDetalle(IngresoEntidad ingreso);

    }
}
