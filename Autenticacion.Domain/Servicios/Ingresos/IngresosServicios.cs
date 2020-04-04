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

        public IngresosServicios(IIngresoRepositorio ingresoRepositorio)
        {

            _ingresoRepositorio = ingresoRepositorio;

        }

        public async Task<IngresoEntidad> CrearIngresoDetalle(IngresoEntidad ingreso)
        {


            //ingreso.DetalleIngresos = new List<DetalleIngresoEntidad> {
            //    new DetalleIngresoEntidad
            //        {
            //            ProductosId = Guid.NewGuid(),
            //            Cantidad = 4,
            //            Precio = 20,
            //            Estatus = "Act"
            //        }
            //};
 
            //await _ingresoRepositorio.CrearDetalleIngreso();

            return await _ingresoRepositorio.CrearIngresoDetalle(ingreso);

        }

        public async Task<bool> EliminarIngresoAsync(Guid id)
        {

            var ingreso = await _ingresoRepositorio.ObtenerIngresoPorIdAsync(id);
            ingreso.Estatus = "Baj";

           return await _ingresoRepositorio.ActualizarIngresoAsync(ingreso);
          
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
