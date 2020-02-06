﻿
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Autenticacion.Dominio.DTO.Respuestas.v1;
using Autenticacion.Dominio.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Servicios.Roles;
using AutoMapper;
using Common.Paginacion;

namespace Autenticacion.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
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

        [HttpGet]
        public async Task<ActionResult<List<RolesDTO>>> ObtenerRolesAsync()
        {

            var lstRoles = await _rolesServicios.ObtenerRolesAsync();

            if (lstRoles == null) return NoContent();

            var respuesta = _mapper.Map<List<RolesDTO>>(lstRoles);

            return Ok(new Respuesta<List<RolesDTO>>(respuesta));

        }

        [HttpGet("{id:guid}", Name = "ObtenerRoleId")]
        public async Task<ActionResult<RolesDTO>> ObtenerRoleIdAsync([FromRoute] Guid id)
        {

            var role = await _rolesServicios.ObtenerRoleIdAsync(id);

            if (role == null) return NotFound("Role no encontrado");

            var respuesta = _mapper.Map<RolesDTO>(role);

            return Ok(new Respuesta<RolesDTO>(respuesta));

        }

        [HttpPost]
        public async Task<ActionResult<RolesDTO>> CrearRoleAsync([FromBody] CrearRoleDTO roleDTO)
        {


            var roleExiste = await _rolesServicios.ObtenerRoleNombreAsync(roleDTO.Nombre);

            if (roleExiste != null) return BadRequest("El role ya se ha registrado anteriormente");

            var role = _mapper.Map<RolesEntidad>(roleDTO);

            var roleCreado = await _rolesServicios.CrearRoleAsync(role);

            if (roleCreado == null) return BadRequest("El role no se ha podido registrar");

            var roleCreadoDTO = _mapper.Map<RolesDTO>(roleCreado);

            var respuesta = new Respuesta<RolesDTO>(roleCreadoDTO);

            return CreatedAtRoute("ObtenerRoleId", new { id = roleCreado.Id }, respuesta);

        }

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

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> EliminarRoleIdAsync([FromRoute] Guid id)
        {

            var roleExiste = await _rolesServicios.ObtenerRoleIdAsync(id);

            if (roleExiste == null) return BadRequest("Role no encontrado");

            var roleEliminado = await _rolesServicios.EliminarRoleAsync(roleExiste);

            if (!roleEliminado) return BadRequest("El rol no se pudo eliminar");

            var respuesta = _mapper.Map<RolesDTO>(roleExiste);

            return Ok(new Respuesta<RolesDTO>(respuesta)); 

        }


    }
}