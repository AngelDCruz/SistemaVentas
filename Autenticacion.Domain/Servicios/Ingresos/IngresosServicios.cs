using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.ModelStore;
using SistemaVentas.Dominio.Repositorio;

namespace SistemaVentas.Dominio.Servicios.Ingresos
{
    public class IngresosServicios : IIngresosServicios
    {

        private readonly IIngresoRepositorio _ingresoRepositorio;
        private readonly IDetallesIngresoRepositorio _detallesIngresoRepositorio;

        public IngresosServicios(
            IIngresoRepositorio ingresoRepositorio,
            IDetallesIngresoRepositorio detallesIngresoRepositorio
         )
        {

            _ingresoRepositorio = ingresoRepositorio;
            _detallesIngresoRepositorio = detallesIngresoRepositorio;

        }

        public async Task<IngresoEntidad> CrearIngresoDetalle(IngresoEntidad ingreso)
        {

            return await _ingresoRepositorio.CrearIngresoDetalle(ingreso);

        }

        public async Task<bool> EliminarIngresoAsync(Guid id)
        {

            var ingreso = await _ingresoRepositorio.ObtenerIngresoPorIdAsync(id);
            ingreso.Estatus = "Baj";

           return await _ingresoRepositorio.ActualizarIngresoAsync(ingreso);
          
        }

        public Task<List<FacturaDetalleIngreso>> ObtenerDetallesIngresosIdAsync(Guid idIngreso)
        {

            return _detallesIngresoRepositorio.ObtenerDetallesIngresosIdAsync(idIngreso);

        }

        public async Task<FacturaIngreso> ObtenerIngresoIdAsync(Guid Id)
        {

            return await _ingresoRepositorio.ObtenerIngresoIdAsync(Id);

        }

        public async Task<List<FacturaIngreso>> ObtenerIngresosAsync()
        {

            return await _ingresoRepositorio.ObtenerIngresoAsync();

        }
    }
}
