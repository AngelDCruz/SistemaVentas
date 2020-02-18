using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autenticacion.Api.Helpers.Sesiones;
using Autenticacion.Api.Servicios.Usuarios;
using Autenticacion.Dominio.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Servicios.Autenticacion;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SessionExtensions = Microsoft.AspNetCore.Http.SessionExtensions;

namespace Autenticacion.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {

        private readonly IAutenticacionServicios _autenticacionServicios;
        private readonly IUsuariosServicios _usuariosServicios;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AutenticacionController(
            IAutenticacionServicios autenticacionServicios,
            IUsuariosServicios usuariosServicios,
            IHttpContextAccessor httpContextAccessor
         )
        {

            _autenticacionServicios = autenticacionServicios;
            _usuariosServicios = usuariosServicios;
            _httpContextAccessor = httpContextAccessor;
        }

       
        [HttpPost("iniciar-sesion")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {

           if( await  _usuariosServicios.ObtenerUsuarioEmailAsync(login.Login) == null)
            {

                return BadRequest("La cuenta no ha sido registrada anteriormente, sigue los pasos para el procedimiento de registro o contacte directamente con su proveedor");
                
            }
         
            var autenticacion = await _autenticacionServicios.Login(login.Login, login.Password);

            if (!autenticacion.Valid && autenticacion.Message != "") return BadRequest(autenticacion.Message);

            var token = new
            {

                access_token = autenticacion.Access_Token,
                refresh_token = autenticacion.Refresh_Token,
                expiracion = autenticacion.Expire,
                type = autenticacion.Type

            };
               return Ok(token);

        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenDTO refreshToken)
        {

            var autenticacion = await _autenticacionServicios.RefreshTokenAsync(refreshToken.Access_Token, refreshToken.Refresh_Token);

            return Ok(autenticacion);

        }

    }
}