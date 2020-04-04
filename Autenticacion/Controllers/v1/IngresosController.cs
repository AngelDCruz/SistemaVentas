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

        public IngresosController(
            IIngresosServicios ingresosServicios,
            IProveedoresServicios proveedoresServicios
         )
        {

            _ingresosServicios = ingresosServicios;
            _proveedoresServicios = proveedoresServicios;

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
        public async Task<ActionResult<IngresoEntidad>> CrearIngresosAsync([FromBody] CrearIngresoDTO ingresoDTO)
        {


            var proveedor = await _proveedoresServicios.ObtenerProveedorIdAsync(ingresoDTO.PersonaId);

            if (proveedor == null) return NotFound("Proveedor no encontrado");

            var ingreso = IngresoMapper.Map(ingresoDTO);

            var respuesta = await _ingresosServicios.CrearIngresoDetalle(ingreso);

            if(respuesta == null) return BadRequest("El ingreso no pudo crearse correctamente");

            return Ok(IngresoMapper.Map(ingreso));

        }

    }
}