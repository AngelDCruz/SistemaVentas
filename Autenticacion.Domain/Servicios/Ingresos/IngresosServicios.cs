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


            var ingresos = new IngresoEntidad();

            var id = Guid.NewGuid();

            ingresos.Id = id;
            ingresos.TipoComprobante = "Factura";
            ingresos.SerieComprobante = "AKASASJS12AS";
            ingresos.Impuesto = 16;
            ingresos.Total = 1200;

            ingresos.UsuariosId = Guid.NewGuid();
            ingresos.Estatus = "Act";
            //ingreso.DetalleIngresos = new List<DetalleIngresoEntidad> {
            //    new DetalleIngresoEntidad
            //        {
            //            ProductosId = Guid.NewGuid(),
            //            Cantidad = 4,
            //            Precio = 20,
            //            Estatus = "Act"
            //        }
            //};
            ////ingresos.Personas = new PersonaEntidad
            //{
            //    Id = Guid.NewGuid(),
            //    Direccion = "asdasdasd",
            //    Email = "test@test.com",
            //    Telefono = "1231231231",
            //    Nombre = "asjasdnsd dasdasdasd",
            //    TipoPersona = "proveedores",
            //    TipoDocumento = "CURP",
            //    NumDocumento = "sKASDK12KSDAS",
            //    Estatus = "Act"
            //};
            //await _ingresoRepositorio.CrearDetalleIngreso();

            return await _ingresoRepositorio.CrearIngresoDetalle(ingresos);

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
