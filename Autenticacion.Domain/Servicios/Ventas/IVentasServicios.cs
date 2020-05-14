using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.ModelStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Dominio.Servicios.Ventas
{
    public interface IVentasServicios
    {

        Task<bool> CrearVentaAsync(VentaEntidad venta);

        Task<bool> EliminarVentaAsync(Guid Id);

        Task<List<FacturaVenta>> ObtenerVentasAsync();

        Task<FacturaVenta> ObtenerVentaPorIdAsync(Guid id);

        Task<List<FacturaDetalleVenta>> ObtenerDetallesVentaPorIdAsync(Guid idVenta);

        Task<decimal> TotalVentasDiaAsync();

        Task<List<VentaUltimos10Dias>> TotalVentas10Dias();

    }
}
