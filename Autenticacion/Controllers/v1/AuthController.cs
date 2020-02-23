
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Autenticacion.Api.Servicios.Usuarios;
using Autenticacion.Dominio.Servicios.Autenticacion;
using Autenticacion.Aplicacion.DTO.Solicitudes.v1;

using Common.Decoradores;

namespace Autenticacion.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAutenticacionServicios _autenticacionServicios;
        private readonly IUsuariosServicios _usuariosServicios;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(
            IAutenticacionServicios autenticacionServicios,
            IUsuariosServicios usuariosServicios,
            IHttpContextAccessor httpContextAccessor
         )
        {

            _autenticacionServicios = autenticacionServicios;
            _usuariosServicios = usuariosServicios;
            _httpContextAccessor = httpContextAccessor;
        }

        [LlaveAutorizacion]
        [HttpPost]
        public async Task<ActionResult> Login([FromHeader] string grant_type ,[FromBody] LoginDTO login)
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

        [LlaveAutorizacion]
        [HttpPut]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenDTO refreshToken)
        {

            var autenticacion = await _autenticacionServicios.RefreshTokenAsync(refreshToken.Access_Token, refreshToken.Refresh_Token);

            return Ok(autenticacion);

        }

    }
}