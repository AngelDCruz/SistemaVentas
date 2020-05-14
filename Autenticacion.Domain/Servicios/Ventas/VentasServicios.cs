using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.ModelStore;
using SistemaVentas.Dominio.Repositorio;

namespace SistemaVentas.Dominio.Servicios.Ventas
{
    public class VentasServicios : IVentasServicios
    {

        private readonly IVentasRepositorio _ventasRepositorio;
        private readonly IDetalleVentaRepositorio _detalleVentaRepositorio;
       
        public VentasServicios(
            IVentasRepositorio ventasRepositorio,
            IDetalleVentaRepositorio detalleVentaRepositorio
        )
        {
            _ventasRepositorio = ventasRepositorio;
            _detalleVentaRepositorio = detalleVentaRepositorio;
        }

        public async Task<bool> CrearVentaAsync(VentaEntidad venta)
        {
            return await _ventasRepositorio.CrearVentaAsync(venta);
        }

        public async Task<bool> EliminarVentaAsync(Guid Id)
        {

            var venta = await _ventasRepositorio.ObtenerVentaPorIdAsync(Id);

            return await _ventasRepositorio.EliminarVentaAsync(venta);
        }

        public async Task<List<FacturaDetalleVenta>> ObtenerDetallesVentaPorIdAsync(Guid idVenta)
        {
            return await _detalleVentaRepositorio.ObtenerDetallesVentaPorIdAsync(idVenta);
        }

        public async Task<FacturaVenta> ObtenerVentaPorIdAsync(Guid id)
        {
            return await _ventasRepositorio.ObtenerVentasPorIdAsync(id);
        }

        public async Task<List<FacturaVenta>> ObtenerVentasAsync()
        {
            return await _ventasRepositorio.ObtenerVentasAsync();
        }


        public async Task<decimal> TotalVentasDiaAsync()
        {
            return await _ventasRepositorio.TotalVentasDiaAsync();
        }


        public async Task<List<VentaUltimos10Dias>> TotalVentas10Dias()
        {
            return await _ventasRepositorio.TotalUltimos10DiasAsync();
        }

    }
}
