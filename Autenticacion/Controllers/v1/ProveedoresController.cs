using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Paginacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Aplicacion.DTO.Respuestas.v1;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Servicios.Proveedores;

namespace SistemaVentas.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {

        private readonly IProveedoresServicios _proveedoresServicios;
        private readonly IMapper _mapper;

        public ProveedoresController(
            IProveedoresServicios proveedoresServicios,
            IMapper mapper
        )
        {
            _proveedoresServicios = proveedoresServicios;
            _mapper = mapper;
        }

        /// <summary>
        /// LISTA DE PROVEEDORES
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ObtenerProveedoresAsync([FromQuery] FiltroPagina filtro, [FromQuery] string estatus = "todos")
        {

            var lstClientes = await _proveedoresServicios.ObtenerProveedoresAsync(filtro, estatus);

            if (lstClientes == null) return NoContent();

            return Ok(_mapper.Map<List<PersonasDTO>>(lstClientes));

        }

        /// <summary>
        /// OBTIENE PROVEEDOR POR ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:guid}", Name = "ObtenerProveedorIdAsync")]
        public async Task<ActionResult> ObtenerProveedorIdAsync([FromRoute] Guid Id)
        {

            var persona = await _proveedoresServicios.ObtenerProveedorIdAsync(Id);

            if (persona == null) return NotFound("Cliente no encontrado");

            return Ok(_mapper.Map<PersonasDTO>(persona));

        }

        /// <summary>
        /// CREAR PROVEEEDOR
        /// </summary>
        /// <param name="crearPersonaDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CrearProveedorAsync([FromBody] CrearPersonaDTO crearPersonaDTO)
        {

            var personaExiste = await _proveedoresServicios.ObtenerProveedorNumDocumento(crearPersonaDTO.NumDocumento);

            if (personaExiste != null) return BadRequest("Ya se ha registrado anteriormente un cliente con este mismo número de documento");

            var crearPersona = _mapper.Map<PersonaEntidad>(crearPersonaDTO);

            var respuesta = await _proveedoresServicios.CrearProveedorAsync(crearPersona);

            if (respuesta == null) return BadRequest("El cliente no pudo crearse correctamente");

            var clienteRespuestDTO = _mapper.Map<PersonasDTO>(respuesta);

            return CreatedAtRoute("ObtenerProveedorIdAsync", new { id = clienteRespuestDTO.Id }, clienteRespuestDTO);

        }

        /// <summary>
        /// ACTUALIZAR PROVEEDOR
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="actualizarClienteDTO"></param>
        /// <returns></returns>
        [HttpPut("{Id:guid}")]
        public async Task<ActionResult> ActualizarProveedorAsync([FromRoute] Guid Id, [FromBody] ActualizarPersonaDTO actualizarClienteDTO)
        {

            if (Id != actualizarClienteDTO.Id) return BadRequest("El modelo no es válido");

            var existeCliente = await _proveedoresServicios.ObtenerProveedorIdAsync(Id);

            if (existeCliente == null) return NotFound("El cliente no existe");

            var clienteEntidad = _mapper.Map<PersonaEntidad>(actualizarClienteDTO);

            var respuesta = await _proveedoresServicios.ActualizarProveedorAsync(clienteEntidad);

            if (!respuesta) return BadRequest("El cliente no se pudo actualizar correctamente");

            return NoContent();

        }

        /// <summary>
        /// ELIMINAR PROVEEDOR
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult> EliminarProveedorAsync([FromRoute] Guid Id)
        {

            var existeCliente = await _proveedoresServicios.ObtenerProveedorIdAsync(Id);

            if (existeCliente == null) return NotFound("Cliente no encontrado");

            var respuesta = await _proveedoresServicios.EliminarProveedorAsync(existeCliente);

            if (!respuesta) return BadRequest("El cliente no pudo eliminarse correctamente");

            return NoContent();

        }

        /// <summary>
        /// ACTIVAR PROVEEDOR
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:guid}/activar-proveedor")]
        public async Task<ActionResult> ActivarProveedorAsync([FromRoute] Guid Id)
        {

            var clienteExiste = await _proveedoresServicios.ObtenerProveedorIdAsync(Id);

            if (clienteExiste == null) return NotFound("Cliente no encontrado");

            var respuesta = await _proveedoresServicios.ActivarProveedorAsync(Id);

            if (!respuesta) return BadRequest("El cliente no pudo activarse correctamente");

            return NoContent();

        }

        [HttpPost("filtrar")]
        public async Task<ActionResult> FiltrarProveedoresAsync([FromBody]  FiltroPersonaDTO filtro)
        {

            var lstProveedores = await _proveedoresServicios.ObtenerFiltroProveedor(filtro);

            if (lstProveedores == null) return NoContent();

            return Ok(_mapper.Map<List<PersonaEntidad>>(lstProveedores));

        }


    }
}