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
using SistemaVentas.Dominio.Servicios.Clientes;
using SistemaVentas.Dominio.Servicios.Productos;
using SistemaVentas.Dominio.Servicios.Ventas;

namespace SistemaVentas.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VentasController : ControllerBase
    {

        private readonly IVentasServicios _ventasServicios;
        private readonly IClientesServicios _clientesServicios;
        private readonly IProductosServicios _productosServicios;

        public VentasController(
            IVentasServicios ventasServicios,
            IClientesServicios clientesServicios,
            IProductosServicios productosServicios
         )
        {
            _ventasServicios = ventasServicios;
            _clientesServicios = clientesServicios;
            _productosServicios = productosServicios;
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerVentasAsync()
        {

            var lstVentas = await _ventasServicios.ObtenerVentasAsync();

            if (lstVentas == null) return NoContent();

            return Ok(lstVentas);

        }

        [HttpPost]
        public async Task<ActionResult> CrearVentaAsync([FromBody] CrearVentaDTO crearVentaDTO)
        {

            var cliente = await _clientesServicios.ObtenerClienteIdAsync(crearVentaDTO.PersonaId);

            if (cliente == null) return BadRequest("Cliente no encontrado");

            if (crearVentaDTO.Total <= 0) return BadRequest("El total de ventas no puede ser cero, verifique la venta");

            if (crearVentaDTO.Impuesto <= 0) return BadRequest("El impuesto no puede ser cero, verifique el impuesto");

            var ventaEntidad = VentaMapper.Map(crearVentaDTO);

            foreach(var detalle in ventaEntidad.DetalleVentas)
            {

                if(await _productosServicios.ObtenerProductoPorIdAsync(detalle.ProductoId) == null)
                {

                    return BadRequest("Uno o más productos no estan registrados en la base de datos");

                }

                if (detalle.Precio <= 0)
                {

                    return BadRequest("El precio del producto no puede ser cero, verifique el precio porfavor");

                }

                if (detalle.Cantidad <= 0) return BadRequest("La cantidad del producto no puede ser cero, verifiquelo porfavor");

            }

            var respuesta = await _ventasServicios.CrearVentaAsync(ventaEntidad);

            if (!respuesta) return BadRequest("La venta no pudo realizarse correctamente");

            return Ok();

        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult> ObtenerVentasAsync([FromRoute] Guid Id)
        {

            var venta = await _ventasServicios.ObtenerVentaPorIdAsync(Id);

            if (venta == null) return NotFound("Venta no encontrada");

            return Ok(venta);

        }

        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult> EliminarVentaAsync([FromRoute] Guid Id)
        {

            var venta = await _ventasServicios.ObtenerVentaPorIdAsync(Id);

            if (venta == null) return NotFound("Venta no encontrada");

            var respuesta = await _ventasServicios.EliminarVentaAsync(Id);

            if (!respuesta) return BadRequest("La venta no pudo eliminarse correctamente");

            return NoContent();

        }

        [HttpGet("{Id:guid}/detalles-venta")]
        public async Task<ActionResult> ObtenerDetallesVentasAsync([FromRoute] Guid Id)
        {

            var detalles = await _ventasServicios.ObtenerDetallesVentaPorIdAsync(Id);

            if (detalles == null) return NotFound("Venta no encontrada");

            return Ok(detalles);

        }

        [HttpGet("venta-dia")]
        public async Task<ActionResult> VentaDiaAsync()
        {

            var venta = await _ventasServicios.TotalVentasDiaAsync();

            return Ok(venta);

        }

        [HttpGet("venta-dias")]
        public async Task<ActionResult> VentaUltimos10DiaAsync()
        {

            var venta = await _ventasServicios.TotalVentas10Dias();

            return Ok(venta);

        }


    }
}