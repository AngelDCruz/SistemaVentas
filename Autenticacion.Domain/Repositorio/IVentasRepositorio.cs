using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.ModelStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Dominio.Repositorio
{
    public interface IVentasRepositorio
    {

        Task<List<FacturaVenta>> ObtenerVentasAsync();

        Task<VentaEntidad> ObtenerVentaPorIdAsync(Guid Id);

        Task<FacturaVenta> ObtenerVentasPorIdAsync(Guid id);

        Task<bool> CrearVentaAsync(VentaEntidad venta);

        Task<bool> EliminarVentaAsync(VentaEntidad venta);

        Task<decimal> TotalVentasDiaAsync();

        Task<List<VentaUltimos10Dias>> TotalUltimos10DiasAsync();

    }
}
