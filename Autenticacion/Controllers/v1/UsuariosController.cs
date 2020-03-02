﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Common.Paginacion;
using AutoMapper;

using SistemaVentas.DTO.Solicitudes.v1;
using SistemaVentas.Dominio.Servicios.Roles;
using SistemaVentas.Api.Servicios.Usuarios;
using SistemaVentas.DTO.Respuestas.v1;
using SistemaVentas.Dominio.Entidades;

namespace Autenticacion.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly IUsuariosServicios _usuariosServicios;
        private readonly IRolesServicios _rolesServicios;
        private readonly IMapper _mapper;

        public UsuariosController(
            IUsuariosServicios usuariosServicios,
            IRolesServicios rolesServicios,
            IMapper mapper
         )
        {

            _usuariosServicios = usuariosServicios;
            _rolesServicios = rolesServicios;
            _mapper = mapper;

        }

        /*
         * USUARIOS
         */
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuariosAsync([FromQuery] IncluirUsuariosDTO incluir,
                                                  [FromQuery] FiltroPagina filtro = null)
        {

            var lstUsuarios = await _usuariosServicios.ObtenerUsuariosAsync(incluir, filtro);

            if (lstUsuarios == null) return NoContent();

            return Ok(lstUsuarios);
            
        }

        [HttpGet("{id:Guid}", Name = "ObtenerUsuarioId")]
        public async Task<IActionResult> ObtenerUsuarioIdAsync(Guid id)
        {

            var usuario = await _usuariosServicios.ObtenerUsuarioIdAsync(id);

            if (usuario == null) return NotFound("Usuario no encontrado");

            return Ok(_mapper.Map<UsuariosDTO>(usuario));

        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuarioAsync([FromBody] CrearUsuarioDTO usuarioDTO)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuario = _mapper.Map<UsuariosEntidad>(usuarioDTO);

            if (await _usuariosServicios.ObtenerUsuarioEmailAsync(usuario.Email) != null)
            {

                return BadRequest("El correo electronico ya esta registrado");

            }

            var crearUsuario = await _usuariosServicios.CrearUsuarioAsync(usuario);

            if (crearUsuario.Equals(Guid.Empty))
            {

                return BadRequest("El usuario no se creo correctamente");

            }

            var usuarioCreadoDTO = _mapper.Map<UsuariosDTO>(usuario);

            return CreatedAtRoute(
                "ObtenerUsuarioId", 
                new { id = crearUsuario },
                usuarioCreadoDTO
             );

        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> ActualizarUsuarioAsync([FromRoute] Guid id, [FromBody] ActualizarUsuarioDTO usuarioDTO)
        {

            if (id != usuarioDTO.Id) return BadRequest("Modelo no valido");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _usuariosServicios.ObtenerUsuarioIdAsync(id) == null) return BadRequest("Usuario no encontrado");

            var usuario = _mapper.Map<UsuariosEntidad>(usuarioDTO);

            var actualizaUsuario = await _usuariosServicios.ActualizarUsuarioAsync(usuario);

            if (!actualizaUsuario) return BadRequest("El usuario no se actualizo correctamente");

            return NoContent();

        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<UsuariosDTO>> EliminarUsuarioAsync([FromRoute] Guid id)
        {

            var usuarioExiste = await _usuariosServicios.ObtenerUsuarioIdAsync(id);

            if (usuarioExiste == null) return BadRequest("El usuario no existe");

            var respuesta = await _usuariosServicios.EliminarUsuarioAsync(usuarioExiste);

            if (!respuesta) return BadRequest("El usuario no se pudo eliminar");

            return Ok(_mapper.Map<UsuariosDTO>(usuarioExiste));

        }

        /*
         * USUARIOS ROLES
         */
        [HttpGet("role/{idRole:guid}")]
         public async Task<ActionResult> ObtenerUsuariosRolesIdAsync([FromRoute] Guid idRole)
        {

            
            var lstUsuarios = await _usuariosServicios.ObtenerUsuariosRoleIdAsync(idRole);

            if (lstUsuarios == null) return NoContent();

            return Ok(lstUsuarios);

        }
    

         [HttpGet("{id:guid}/roles")]
         public async Task<ActionResult> ObtenerUsuarioRoles([FromRoute] Guid id)
        {

            var usuario = await _usuariosServicios.ObtenerUsuarioIdAsync(id);

            if (usuario == null) return BadRequest("Usuario no encontrado");

            return Ok(await _usuariosServicios.ObtenerUsuarioIdRoleAsync(id));

        }


        [HttpPost("asignar-role")]
        public async Task<IActionResult> AsignarRolesUsuariosAsync([FromBody] CrearUsuarioRolesDTO usuarioDTO)
        {


            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _usuariosServicios.ObtenerUsuarioIdAsync(usuarioDTO.IdUsuario) == null)
            {

                return BadRequest("Usuario no encontrado");

            }

            var crearUsuario = await _usuariosServicios.CrearUsuarioRoleAsync(usuarioDTO);

            if (!crearUsuario) return BadRequest("No se asigno ningun role al usuario");



            return Ok();

        }

        [HttpDelete("{idUsuario:guid}/role/{idRole:guid}")]
        public async Task<ActionResult> EliminarUsuarioRole([FromRoute] Guid idUsuario, Guid idRole)
        {

            var usuarioRoleExiste = await _usuariosServicios.ObtenerUsuarioRoleAsync(idRole, idUsuario);

            if (usuarioRoleExiste == null) return BadRequest("El usuario no tiene asignado este rol");

            if (!_usuariosServicios.EliminarUsuarioRoleAsync(usuarioRoleExiste))
            {

                return BadRequest("El rol actual asignado a este usuario no pudo eliminarse");

            }

            return NoContent();

        }

       

    }
}