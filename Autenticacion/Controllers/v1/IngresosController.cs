using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Api.Mapper.Personalizados;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Servicios.Ingresos;
using SistemaVentas.Dominio.Servicios.Productos;
using SistemaVentas.Dominio.Servicios.Proveedores;

namespace SistemaVentas.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IngresosController : ControllerBase
    {

        private readonly IIngresosServicios _ingresosServicios;
        private readonly IProveedoresServicios _proveedoresServicios;
        private readonly IProductosServicios _productosServicios;

        public IngresosController(
            IIngresosServicios ingresosServicios,
            IProveedoresServicios proveedoresServicios,
            IProductosServicios productosServicios
         )
        {

            _ingresosServicios = ingresosServicios;
            _proveedoresServicios = proveedoresServicios;
            _productosServicios = productosServicios;

        }

        /// <summary>
        ///  LISTA DE INGRESOS
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ObtenerIngresosAsync()
        {

            var lstIngresos = await _ingresosServicios.ObtenerIngresosAsync();

            if (lstIngresos == null) return NoContent();

            return Ok(lstIngresos);

        }

        /// <summary>
        ///  OBTIENE INGRESO CON ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:guid}")]
        public async Task<ActionResult> ObtenerIngresoIdAsync([FromRoute] Guid Id)
        {

            var ingreso = await _ingresosServicios.ObtenerIngresoIdAsync(Id);

            if (ingreso == null) return NotFound("Ingreso no encontrado");

            return Ok(ingreso);

        }

        /// <summary>
        /// ELIMINA INGRESO CON EL ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult> EliminarIngresoAsync([FromRoute] Guid Id)
        {

            var ingreso = await _ingresosServicios.ObtenerIngresoIdAsync(Id);

            if (ingreso == null) return NotFound("Ingreso no encontrado");

            var respuesta = await _ingresosServicios.EliminarIngresoAsync(Id);

            if (!respuesta) return BadRequest("El ingreso no pudo darse de baja correctamente");

            return NoContent();

        }

        /// <summary>
        /// CREA UN INGRESO
        /// </summary>
        /// <param name="ingresoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CrearIngresosAsync([FromBody] CrearIngresoDTO ingresoDTO)
        {


            var proveedor = await _proveedoresServicios.ObtenerProveedorIdAsync(ingresoDTO.PersonaId);

            if (proveedor == null) return NotFound("Proveedor no encontrado");

            if (ingresoDTO.Detalles == null) return BadRequest("El detalle del ingreso debe contener uno o más productos");

            foreach(var detalle in ingresoDTO.Detalles)
            {

                if(await _productosServicios.ObtenerProductoPorIdAsync(detalle.ProductoId) == null)
                {
                    return BadRequest("Uno o más productos no estan registrados en base de datos. Intentelo de nuevo");
                }

            }

            var ingreso = IngresoMapper.Map(ingresoDTO);

            var respuesta = await _ingresosServicios.CrearIngresoDetalle(ingreso);

            if(respuesta == null) return BadRequest("El ingreso no pudo crearse correctamente");

            return Ok();

        }

        /// <summary>
        /// OBTIENE LISTA DE DETALLES
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:guid}/detalle-ingresos")]
        public async Task<ActionResult> ObtenerDetallesIngresoIdAsync([FromRoute] Guid Id)
        {

            var lstDetalleIngresos = await _ingresosServicios.ObtenerDetallesIngresosIdAsync(Id);

            if (lstDetalleIngresos == null) return NotFound("No existen detalles de ingresos");

            return Ok(lstDetalleIngresos);

        }


    }
}