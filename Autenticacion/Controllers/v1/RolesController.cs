
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Servicios.Roles;

using Autenticacion.Aplicacion.DTO.Respuestas.v1;
using Autenticacion.Aplicacion.DTO.Solicitudes.v1;

using AutoMapper;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;

namespace Autenticacion.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolesController : ControllerBase
    {

        private readonly IRolesServicios _rolesServicios;
        private readonly IMapper _mapper;

        public RolesController(
            IRolesServicios rolesServicios,
            IMapper mapper
        )
        {

            _rolesServicios = rolesServicios;
            _mapper = mapper;

        }

        /// <summary>
        /// OBTIENE LISTA DE ROLES 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<RolesDTO>>> ObtenerRolesAsync()
        {

            var lstRoles = await _rolesServicios.ObtenerRolesAsync();

            if (lstRoles == null) return NoContent();

            return Ok(_mapper.Map<List<RolesDTO>>(lstRoles));

        }

        /// <summary>
        /// OBTIENE ROL POR SU IDENTIFICADOR
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id:guid}", Name = "ObtenerRoleId")]
        public async Task<ActionResult<RolesDTO>> ObtenerRoleIdAsync([FromRoute] Guid id)
        {

            var role = await _rolesServicios.ObtenerRoleIdAsync(id);

            if (role == null) return NotFound("Role no encontrado");

            return Ok(_mapper.Map<RolesDTO>(role));

        }

        /// <summary>
        /// CREA UN ROL
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<RolesDTO>> CrearRoleAsync([FromBody] CrearRoleDTO roleDTO)
        {


            var roleExiste = await _rolesServicios.ObtenerRoleNombreAsync(roleDTO.Nombre);

            if (roleExiste != null) return BadRequest("El role ya se ha registrado anteriormente");

            var role = _mapper.Map<RolesEntidad>(roleDTO);

            var roleCreado = await _rolesServicios.CrearRoleAsync(role);

            if (roleCreado == null) return BadRequest("El role no se ha podido registrar");

            var roleCreadoDTO = _mapper.Map<RolesDTO>(roleCreado);

            return CreatedAtRoute("ObtenerRoleId", new { id = roleCreado.Id }, roleCreadoDTO);

        }

        /// <summary>
        /// ACTUALIZA UN ROL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> ActualizarRoleAsync([FromRoute] Guid id, [FromBody] ActualizarRoleDTO roleDTO)
        {

            if (id != roleDTO.Id) return BadRequest("El modelo no es válido");

            var roleExiste = await _rolesServicios.ObtenerRoleIdAsync(id);

            if (roleExiste == null) return BadRequest("Role no encontrado");

            var role = _mapper.Map<RolesEntidad>(roleDTO);

            var respuesta = await _rolesServicios.ActualizarRoleAsync(role);

            if (!respuesta) return BadRequest("El rol no pudo actualizarse");

            return NoContent();

        }

        /// <summary>
        /// ELIMINA UN ROLE POR SU IDENTIFICADOR
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> EliminarRoleIdAsync([FromRoute] Guid id)
        {

            var roleExiste = await _rolesServicios.ObtenerRoleIdAsync(id);

            if (roleExiste == null) return BadRequest("Role no encontrado");

            var roleEliminado = await _rolesServicios.EliminarRoleAsync(roleExiste);

            if (!roleEliminado) return BadRequest("El rol no se pudo eliminar");

            return Ok(_mapper.Map<RolesDTO>(roleExiste)); 

        }

        /// <summary>
        ///  ACTIVA ROLE MEDIANTE EL ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:guid}/activar-role")]
        public async  Task<ActionResult> ActivarRoleAsync([FromRoute] Guid Id)
        {

            var role = await _rolesServicios.ObtenerRoleIdAsync(Id);

            if (role == null) return NotFound("Role no encontrado");

            var respuesta = await _rolesServicios.ActivarRolePorIdAsync(Id);

            if (!respuesta) return BadRequest("El rol no se pudo activar correctamente");

            return NoContent();

        }

        [HttpPost("busqueda")]
        public async Task<ActionResult> BusquedaRoleNombre([FromBody] FiltroRoleDTO roleDTO)
        {

            var lstRoles = await _rolesServicios.BusquedaRoleAsync(roleDTO.Nombre);

            if (lstRoles == null) return NoContent();

            return Ok(_mapper.Map<List<RolesDTO>>(lstRoles));

        }

    }
}