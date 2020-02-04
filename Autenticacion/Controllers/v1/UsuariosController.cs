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
        private readonly IMapper _mapper;

        public UsuariosController(
            IUsuariosServicios usuariosServicios,
            IMapper mapper
         )
        {

            _usuariosServicios = usuariosServicios;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuariosAsync()
        {

            var lstUsuarios = await _usuariosServicios.ObtenerUsuariosAsync();

            if (lstUsuarios == null) return NoContent();

            var lstUsuariosDTO = _mapper.Map<List<UsuariosRespuestasDTO>>(lstUsuarios);

            return Ok(lstUsuariosDTO);

        }

        [HttpGet("{id:Guid}", Name = "ObtenerUsuarioId")]
        public async Task<IActionResult> ObtenerUsuarioIdAsync(Guid id)
        {

            var usuario = await _usuariosServicios.ObtenerUsuarioIdAsync(id);

            if (usuario == null) return NotFound("Usuario no encontrado");

            var usuarioDTO = _mapper.Map<UsuariosRespuestasDTO>(usuario);

            return Ok(usuarioDTO);

        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuarioAsync([FromBody] CrearUsuarioDTO usuarioDTO)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuario = _mapper.Map<Usuarios>(usuarioDTO);

            if(await _usuariosServicios.ObtenerUsuarioEmailAsync(usuario.Email) != null)
            {

                return BadRequest("El correo electronico ya esta registrado");

            }

            var crearUsuario = await _usuariosServicios.CrearUsuarioAsync(usuario);

            if (crearUsuario.Equals(Guid.Empty))
            {

                return BadRequest("El usuario no se creo correctamente");

            }

            var usuarioCreadoDTO = _mapper.Map<UsuariosRespuestasDTO>(usuario);

            return CreatedAtRoute("ObtenerUsuarioId", new { id = crearUsuario}, usuarioCreadoDTO);

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> ActualizarUsuarioAsync([FromRoute] Guid id, [FromBody] ActualizarUsuarioDTO usuarioDTO)
        {

            if(id != usuarioDTO.Id )  return BadRequest("Modelo no valido");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (await _usuariosServicios.ObtenerUsuarioIdAsync(id) == null) return BadRequest("Usuario no encontrado");

            var usuario = _mapper.Map<Usuarios>(usuarioDTO);

            var actualizaUsuario = await _usuariosServicios.ActualizarUsuarioAsync(usuario);

            if (!actualizaUsuario) return BadRequest("El usuario no se actualizo correctamente");

            return NoContent();

        }


    }
}