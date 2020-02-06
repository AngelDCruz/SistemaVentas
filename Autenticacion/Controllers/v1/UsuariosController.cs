using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autenticacion.Api.DTO.Respuestas.v1;
using Autenticacion.Api.DTO.Solicitudes.v1;
using Autenticacion.Api.Servicios;
using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio.Contratos;
using AutoMapper;
using Common.Paginacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> ObtenerUsuariosAsync([FromQuery] IncluirUsuariosDTO incluir, [FromQuery] FiltroPagina filtro)
        {

            var lstUsuarios = await _usuariosServicios.ObtenerUsuariosAsync(filtro.Limite, filtro.Pagina);

            if (lstUsuarios == null || lstUsuarios.Count <= 0) return NoContent();

            var lstUsuariosPaginados = _usuariosServicios.ObtenerUsuariosRoles(incluir, filtro).ToList();

            var lstDatos = new Paginador<List<UsuariosDTO>>(lstUsuariosPaginados, filtro);


            return Ok(lstDatos);
            
        }

        [HttpGet("{id:Guid}", Name = "ObtenerUsuarioId")]
        public async Task<IActionResult> ObtenerUsuarioIdAsync(Guid id)
        {

            var usuario = await _usuariosServicios.ObtenerUsuarioIdAsync(id);

            if (usuario == null) return NotFound("Usuario no encontrado");

            var usuarioDTO = _mapper.Map<UsuariosDTO>(usuario);

            return Ok(usuarioDTO);

        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuarioAsync([FromBody] CrearUsuarioDTO usuarioDTO)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuario = _mapper.Map<Usuarios>(usuarioDTO);

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

            return CreatedAtRoute("ObtenerUsuarioId", new { id = crearUsuario }, usuarioCreadoDTO);

        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> ActualizarUsuarioAsync([FromRoute] Guid id, [FromBody] ActualizarUsuarioDTO usuarioDTO)
        {

            if (id != usuarioDTO.Id) return BadRequest("Modelo no valido");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _usuariosServicios.ObtenerUsuarioIdAsync(id) == null) return BadRequest("Usuario no encontrado");

            var usuario = _mapper.Map<Usuarios>(usuarioDTO);

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

            var usuarioEliminado = _mapper.Map<UsuariosDTO>(usuarioExiste);

            return Ok(usuarioEliminado);

        }

        ///*
        // * USUARIOS ROLES
        // */
        //[HttpGet("usuarios-roles")]
        //public ActionResult<List<UsuariosRolesDTO>> ObtenerUsuariosRolesAsync()
        //{

        //    var lstUsuariosRoles = _usuariosServicios.ObtenerUsuariosRoles(true);

        //    if (lstUsuariosRoles == null) return NoContent();

        //    return Ok(lstUsuariosRoles);

        //}


        [HttpPost("asignar-role")]
        public async Task<IActionResult> AsignarRolesUsuariosAsync([FromBody] CrearUsuarioRolesDTO usuarioDTO)
        {


            if (!ModelState.IsValid) return BadRequest(ModelState);

            var lstUsuarioRoles = EstructurarRoles(usuarioDTO);

            if (await _usuariosServicios.ObtenerUsuarioIdAsync(usuarioDTO.IdUsuario) == null)
            {

                return BadRequest("Usuario no encontrado");

            }

            bool crearUsuario = false;

            if (lstUsuarioRoles.Count > 0)
            {

                crearUsuario = await _usuariosServicios.CrearUsuarioRoleAsync(lstUsuarioRoles);

            }

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

        private List<UsuariosRoles> EstructurarRoles(CrearUsuarioRolesDTO lstUsuariosRoles)
        {

            List<UsuariosRoles> nvLstUsuariosRoles = new List<UsuariosRoles>();

            if (lstUsuariosRoles == null) return nvLstUsuariosRoles;

            foreach(var role in lstUsuariosRoles.Roles)
            {

            
                nvLstUsuariosRoles.Add(new UsuariosRoles()
                {
                    UserId = lstUsuariosRoles.IdUsuario,
                    UsuariosId = lstUsuariosRoles.IdUsuario,
                    RoleId = role,
                    RolesId = role
                });

            }

            return nvLstUsuariosRoles;

        }


    }
}