using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Servicios.Ingresos;

namespace SistemaVentas.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IngresosController : ControllerBase
    {

        private readonly IIngresosServicios _ingresosServicios;

        public IngresosController(IIngresosServicios ingresosServicios)
        {

            _ingresosServicios = ingresosServicios;

        }


        [HttpGet]
        public async Task<ActionResult> ObtenerIngresosAsync()
        {

            var lstIngresos = await _ingresosServicios.ObtenerIngresosAsync();

            if (lstIngresos == null) return NoContent();

            return Ok(lstIngresos);

        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult> ObtenerIngresoIdAsync([FromRoute] Guid Id)
        {

            var ingreso = await _ingresosServicios.ObtenerIngresoIdAsync(Id);

            if (ingreso == null) return NotFound("Ingreso no encontrado");

            return Ok(ingreso);

        }

        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult> BajaIngresoAsync([FromRoute] Guid Id)
        {

            var ingreso = await _ingresosServicios.ObtenerIngresoIdAsync(Id);

            if (ingreso == null) return NotFound("Ingreso no encontrado");

            var respuesta = await _ingresosServicios.EliminarIngresoAsync(Id);

            if (!respuesta) return BadRequest("El ingreso no pudo darse de baja correctamente");

            return NoContent();

        }

        [HttpPost]
        public async Task<ActionResult<IngresoEntidad>> CrearIngresosAsync([FromBody] IngresoEntidad ingreso)
        {

            return await _ingresosServicios.CrearIngresoDetalle(ingreso);

        }

    }
}