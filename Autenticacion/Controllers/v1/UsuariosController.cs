using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Autenticacion.Api.Mapper.Personalizados;
using AutoMapper;

using Autenticacion.Api.Servicios.Usuarios;

using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Servicios.Roles;
using Autenticacion.Dominio.Servicios.Cuenta;
using Autenticacion.Dominio.DTO.Solicitudes.v1;

using Common.Paginacion;
using Common.Mensajeria;
using Common.Decoradores;

using Autenticacion.Aplicacion.DTO.Solicitudes.v1;
using Autenticacion.Aplicacion.DTO.Respuestas.v1;

namespace Autenticacion.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuariosController : ControllerBase
    {

        private readonly ICuentaServicios _cuentaServicios;
        private readonly IUsuariosServicios _usuariosServicios;
        private readonly IRolesServicios _rolesServicios;
        private readonly IMapper _mapper;

        public UsuariosController(
            ICuentaServicios cuentaServicios,
            IUsuariosServicios usuariosServicios,
            IRolesServicios rolesServicios,
            IMapper mapper
         )
        {

            _usuariosServicios = usuariosServicios;
            _rolesServicios = rolesServicios;
            _cuentaServicios = cuentaServicios;
            _mapper = mapper;

        }

        /// <summary>
        /// OBTIENE LISTA DE USUARIOS
        /// </summary>
        /// <param name="incluir"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuariosAsync([FromQuery] IncluirUsuariosDTO incluir,
                                                  [FromQuery] FiltroPagina filtro = null)
        {

            var lstUsuarios = await _usuariosServicios.ObtenerUsuariosAsync(incluir, filtro);

            if (lstUsuarios == null) return NoContent();

            return Ok(lstUsuarios);

        }

        /// <summary>
        /// OBTIENE UN USUARIO POR SU IDENTIFICADOR
        /// </summary>
        /// <param name="id"></param>
        /// <param name="incluir"></param>
        /// <returns></returns>
        [HttpGet("{id:Guid}", Name = "ObtenerUsuarioId")]
        public async Task<IActionResult> ObtenerUsuarioIdAsync(Guid id, [FromQuery] IncluirUsuariosDTO incluir)
        {

            var usuario = await _usuariosServicios.ObtenerUsuarioIdAsync(id);

            if (usuario == null) return NotFound("Usuario no encontrado");

            var usuarioDTO = await _usuariosServicios.ObtenerUsuarioIdRelacionesAsync(id, incluir);

            return Ok(usuarioDTO);

        }

        /// <summary>
        /// CREA UN USUARIO
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns></returns>
        //[LlaveAutorizacion]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CrearUsuarioAsync([FromBody] CrearUsuarioDTO usuarioDTO)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuario = UsuarioMapper.Map(usuarioDTO);

            if (await _usuariosServicios.ObtenerUsuariosNombreAsync(usuarioDTO.Usuario) != null)
            {

                return BadRequest("El nombre de usuario ya esta registrado intentelo con uno diferente");

            }

            if (await _usuariosServicios.ObtenerUsuarioEmailAsync(usuario.Email) != null)
            {

                return BadRequest("El correo electronico ya esta registrado");

            }

            var idUsuarioCreado = await _usuariosServicios.CrearUsuarioAsync(usuario);


            if (idUsuarioCreado.Equals(Guid.Empty))
            {

                return BadRequest("El usuario no se creo correctamente");

            }

            var usuarioCreado = await _usuariosServicios.ObtenerUsuarioIdAsync(idUsuarioCreado);

            var usuarioCreadoDTO = _mapper.Map<UsuariosDTO>(usuarioCreado);

            var correoEnviado = new Gmail(usuario.Email, "Te has registrado correctamente", "Bienvenido te has registrado en nuestro sistema");
            correoEnviado.Enviar();

            return CreatedAtRoute(
                "ObtenerUsuarioId",
                new { id = idUsuarioCreado },
                usuarioCreadoDTO
             );

        }

        /// <summary>
        /// ELIMINA UN USUARIO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> EliminarUsuarioAsync([FromRoute] Guid id)
        {

            var usuarioExiste = await _usuariosServicios.ObtenerUsuarioIdAsync(id);

            if (usuarioExiste == null) return BadRequest("El usuario no existe");

            var respuesta = await _usuariosServicios.EliminarUsuarioAsync(usuarioExiste);

            if (!respuesta) return BadRequest("El usuario no se pudo eliminar");

            return Ok(_mapper.Map<UsuariosDTO>(usuarioExiste));

        }


        /// <summary>
        /// OBTIENE LISTA DE USUARIOS QUE PERTENECEN A CIERTO ROL
        /// </summary>
        /// <param name="idRole"></param>
        /// <returns></returns>
        [HttpGet("role/{idRole:guid}")]
        public async Task<ActionResult> ObtenerUsuariosRolesIdAsync([FromRoute] Guid idRole)
        {


            var lstUsuarios = await _usuariosServicios.ObtenerUsuariosRoleIdAsync(idRole);

            if (lstUsuarios == null) return NoContent();

            return Ok(lstUsuarios);

        }

        /// <summary>
        /// ASIGNA UN ROL O ROLES A UN USUARIO EN ESPECIFICO
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns></returns>
        [HttpPost("asignar-role")]
        public async Task<IActionResult> AsignarRoleUsuariosAsync([FromBody] CrearUsuarioRolesDTO usuarioDTO)
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

        /// <summary>
        /// ELIMINA EL ROL QUE SE HA ASIGNADO A UN USUARIO
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idRole"></param>
        /// <returns></returns>
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

        


        /// <summary>
        /// RECUPERACION DE CUENTA Y CAMBIOS DE IMAGEN DE PERFIL
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost("cambio-password")]
        public async Task<ActionResult> CambiarPasswordAsync([FromBody] UsuarioCambioPasswordDTO usuario)
        {

            var respuesta = await _cuentaServicios.RestaurarPasswordCuentaAsync(usuario.PasswordAnterior, usuario.PasswordNueva);

            if (!respuesta) return BadRequest("La contrasena no pudo restablecerse correctamente, intentelo de nuevo");

            return NoContent();

        }

        /// <summary>
        /// CAMBIA NOMBRE DE UN USUARIO
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}/cambiar-nombre/{nombre}")]
        public async Task<ActionResult> CambiarNombreUsuarioAsync([FromRoute] Guid id, [FromRoute] string nombre)
        {

            var usuario = await _usuariosServicios.ObtenerUsuarioIdAsync(id);

            if (usuario == null) return NotFound("El usuario no existe");

            bool respuesta = await _cuentaServicios.CambiarNombrePerfilAsync(id, nombre);

            if (!respuesta) return NotFound("El nombre de usuario no se ha podido actualizar");

            return NoContent();

        }

    }
}