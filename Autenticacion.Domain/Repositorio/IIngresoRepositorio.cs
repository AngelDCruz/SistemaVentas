using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.ModelStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Dominio.Repositorio
{
    public interface IIngresoRepositorio
    {

        Task<List<FacturaIngreso>> ObtenerIngresoAsync();

        Task<FacturaIngreso> ObtenerIngresoIdAsync(Guid id);

        Task<IngresoEntidad> ObtenerIngresoPorIdAsync(Guid id);

        Task<bool> EliminarIngresoAsync(IngresoEntidad ingreso);

        Task<bool> ActualizarIngresoAsync(IngresoEntidad ingreso);

        Task<IngresoEntidad> CrearIngresoDetalle(IngresoEntidad ingreso);

        Task<DetalleIngresoEntidad> CrearDetalleIngreso(DetalleIngresoEntidad detalleIngreso);

    }
}
