using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Paginacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Aplicacion.DTO.Respuestas.v1;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Servicios.Clientes;

namespace SistemaVentas.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientesController : ControllerBase
    {

        private readonly IClientesServicios _clientesServicios;
        private readonly IMapper _mapper;

        public ClientesController(
            IClientesServicios clientesServicios,
            IMapper mapper
        )
        {
            _clientesServicios = clientesServicios;
            _mapper = mapper;
        }

        /// <summary>
        /// LISTA DE CLIENTES
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ObtenerClientesAsync([FromQuery] FiltroPagina filtro, [FromQuery] string estatus = "todos")
        {

            var lstClientes = await _clientesServicios.ObtenerClientesAsync(filtro, estatus);

            if (lstClientes == null) return NoContent();

            return Ok(_mapper.Map<List<PersonasDTO>>(lstClientes));

        }

        /// <summary>
        /// OBTIENE CLIENTE POR ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> ObtenerClienteIdAsync([FromRoute] Guid id)
        {

            var persona = await _clientesServicios.ObtenerClienteIdAsync(id);

            if (persona == null) return NotFound("Cliente no encontrado");

            return Ok(_mapper.Map<PersonasDTO>(persona));

        }


        /// <summary>
        /// CREAR CLIENTE
        /// </summary>
        /// <param name="crearPersonaDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CrearClienteAsync([FromBody] CrearPersonaDTO crearPersonaDTO)
        {

            var personaExiste = await _clientesServicios.ObtenerClienteNumDocumento(crearPersonaDTO.NumDocumento);

            if (personaExiste != null) return BadRequest("Ya se ha registrado anteriormente un cliente con este mismo número de documento");

            var crearPersona = _mapper.Map<PersonaEntidad>(crearPersonaDTO);

            var respuesta = await _clientesServicios.CrearClienteAsync(crearPersona);

            if (respuesta == null) return BadRequest("El cliente no pudo crearse correctamente");

            var clienteRespuestDTO = _mapper.Map<PersonasDTO>(respuesta);

            return CreatedAtRoute(nameof(ObtenerClienteIdAsync), new { id = clienteRespuestDTO.Id }, clienteRespuestDTO);

        }

        /// <summary>
        /// ACTUALIZAR CLIENTE
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="actualizarClienteDTO"></param>
        /// <returns></returns>
        [HttpPut("{Id:guid}")]
        public async Task<ActionResult> ActualizarClienteAsync([FromRoute] Guid Id, [FromBody] ActualizarPersonaDTO actualizarClienteDTO)
        {

            if (Id != actualizarClienteDTO.Id) return BadRequest("El modelo no es válido");

            var existeCliente = await _clientesServicios.ObtenerClienteIdAsync(Id);

            if (existeCliente == null) return NotFound("El cliente no existe");

            var clienteEntidad = _mapper.Map<PersonaEntidad>(actualizarClienteDTO);

            var respuesta = await _clientesServicios.ActualizarClienteAsync(clienteEntidad);

            if (!respuesta) return BadRequest("El cliente no se pudo actualizar correctamente");

            return NoContent();

        }

        /// <summary>
        /// ELIMINAR CLIENTE
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult> EliminarClienteAsync([FromRoute] Guid Id)
        {

            var existeCliente = await _clientesServicios.ObtenerClienteIdAsync(Id);

            if (existeCliente == null) return NotFound("Cliente no encontrado");

            var respuesta = await _clientesServicios.EliminarClienteAsync(existeCliente);

            if (!respuesta) return BadRequest("El cliente no pudo eliminarse correctamente");

            return NoContent();

        }

        /// <summary>
        /// ACTIVAR CLIENTE
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:guid}/activar-cliente")]
        public async Task<ActionResult> ActivarClienteAsync([FromRoute] Guid Id)
        {

            var clienteExiste = await _clientesServicios.ObtenerClienteIdAsync(Id);

            if (clienteExiste == null) return NotFound("Cliente no encontrado");

            var respuesta = await _clientesServicios.ActivarClienteAsync(Id);

            if (!respuesta) return BadRequest("El cliente no pudo activarse correctamente");

            return NoContent();

        }

        [HttpPost("filtrar")]
        public async Task<ActionResult> FiltrarClientesAsync([FromBody]  FiltroPersonaDTO filtro)
        {

            var lstClientes = await _clientesServicios.ObtenerFiltroCliente(filtro);

            if (lstClientes == null) return NoContent();

            return Ok(_mapper.Map<List<PersonaEntidad>>(lstClientes));

        }

    }
}