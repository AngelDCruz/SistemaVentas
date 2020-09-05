
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio;

using Autenticacion.Infraestructura.EntidadesConfiguracion;

using Autenticacion.Aplicacion.DTO.Respuestas.v1;
using Common.Excepciones;


using Newtonsoft.Json;

namespace Autenticacion.Dominio.Servicios.Autenticacion
{
    public class AutenticacionServicios : IAutenticacionServicios
    {

        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IConfiguration _configuration;
        private readonly ITokenRepositorio _tokenRepositorio;
        private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly IRolesRepositorio _rolesRepositorio;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AutenticacionServicios(
            ITokenRepositorio tokenRepositorio,
            IUsuariosRepositorio usuariosRepositorio,
            IRolesRepositorio rolesRepositorio,
            TokenValidationParameters tokenValidationParameters,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
         )
        {

            _tokenRepositorio = tokenRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
            _rolesRepositorio = rolesRepositorio;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AutenticacionDTO> Login(string login, string password)
        {
            var tokenGenerado = new AutenticacionDTO();

            var cuenta = await _usuariosRepositorio.ObtenerUsuarioEmailAsync(login);

            var comprobarAccesso = await _usuariosRepositorio.VerificarCredencialesAsync(cuenta, password);

            if (!comprobarAccesso)
            {
                tokenGenerado.Valid = false;
                tokenGenerado.Message = "El usuario o contraseña son invalidos";

                return tokenGenerado;
            }

            tokenGenerado = await GenerarTokenAutorizacion(cuenta);

            tokenGenerado.Valid = true;

            return tokenGenerado;

        }


        #region "LOGICA PARA GENERAR TOKEN"
        /// <summary>
        /// GENERA UN TOKEN Y AL TOKEN SE PASAN RECLAMACIONES NECESARIAS
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        private async Task<AutenticacionDTO> GenerarTokenAutorizacion(UsuariosEntidad usuario)
        {

            var tokenManejador = new JwtSecurityTokenHandler();

            var lstClaims = await ObtenerListaReclamacionesAsync(usuario);

            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);

            var tiempoVida = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["JWT:TokenTiempoVida"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration["JWT:Issuer"],
                Issuer = _configuration["JWT:Issuer"],
                Subject = new ClaimsIdentity(lstClaims),
                Expires = tiempoVida,
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenManejador.CreateToken(tokenDescriptor);

            var nuevoToken = new TokenEntidad()
            {
                JwtId = token.Id,
                UserId = usuario.Id,
                FechaCreacion = DateTime.UtcNow,
                Expiracion = DateTime.UtcNow.AddMonths(6)
            };

            await _tokenRepositorio.CrearTokenAsync(nuevoToken);

            return new AutenticacionDTO
            {
                Access_Token = tokenManejador.WriteToken(token),
                Refresh_Token = nuevoToken.Token,
                Expire = tiempoVida
            };

        }

        /// <summary>
        /// ACTUALIZA EL TOKEN Y EL REFRESH TOKEN DESPUES DE QUE CUMPLE SU CADUCIDAD
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<AutenticacionDTO> RefreshTokenAsync(string token, string refreshToken)
        {

            AutenticacionDTO autenticacionDTO = new AutenticacionDTO();

            var generarToken = GenerarToken(token);

            if(generarToken ==  null)
            {

                throw new Exception("El token erroneo");
                
            }

            var tiempoExpiracionNuevoToken = generarToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value;

            long fechaExpiracionCorto = long.Parse(tiempoExpiracionNuevoToken);

            var tiempoExpiracionCorto = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(fechaExpiracionCorto);

            var jti = generarToken.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;


            if (tiempoExpiracionCorto > DateTime.UtcNow)
            {

                throw new Excepcion("El token aún es válido, no ha expirado");

            }

            var obtenerRefreshToken = await _tokenRepositorio.ObtenerTokenAsync(refreshToken);

            if (obtenerRefreshToken == null)
            {

                throw new Excepcion("El token es invalido, no existe");

            }

            if (obtenerRefreshToken.Expiracion < DateTime.UtcNow)
            {

                throw new Excepcion("El token es invalido, este ha expirado hace poco");

            }

            if (obtenerRefreshToken.Valido)
            {

                throw new Excepcion("El token es invalido, el token ya fue usado");

            }

            if (obtenerRefreshToken.Usado)
            {

                throw new Excepcion("El token es invalido, el token de actualización ya fue usado anteriormente");

            }

            if (obtenerRefreshToken.JwtId != jti)
            {

                throw new Excepcion("El token es invalido, el token de actualización no coincide con jwt");

            }

            if (obtenerRefreshToken.Expiracion < DateTime.UtcNow)
            {

                throw new Excepcion("El token es válido, el token aun no ha expirado");

            }

        
            obtenerRefreshToken.Usado = true;

            await _tokenRepositorio.ActualizarTokenAsync(obtenerRefreshToken);

            Guid idUsuario = Guid.Parse(generarToken.Claims.SingleOrDefault(x => x.Type == "Id").Value);

            var obtenerUsuario = await _usuariosRepositorio.ObtenerUsuarioPorIdAsync(idUsuario);

            return await GenerarTokenAutorizacion(obtenerUsuario);

        }

        /// <summary>
        /// OBTIENE LISTA DE RECLAMACIONES PARA AGREGARLO AL TOKEN DE AUTORIZACION
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        private async Task<List<Claim>> ObtenerListaReclamacionesAsync(UsuariosEntidad usuario)
        {

            List<Claim> lstClaimsToken = new List<Claim>();

            var lstRoles = await _usuariosRepositorio.ObtenerUsuariosRolesAsync(usuario);

            lstClaimsToken.Add(new Claim("Id", usuario.Id.ToString()));
            lstClaimsToken.Add(new Claim("User", usuario.UserName));
            lstClaimsToken.Add(new Claim(ClaimTypes.Email, usuario.Email));
            lstClaimsToken.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            foreach (var usuarioRole in lstRoles)
            {

                //ASIGANACION LISTA DE ROLES
                lstClaimsToken.Add(new Claim(ClaimTypes.Role, usuarioRole));

                var role = await _rolesRepositorio.ObtenerRoleNombreAsync(usuarioRole);

                if (role == null) continue;

                var roleClaims = await _rolesRepositorio.ObtenerRoleClaimAsync(role);

                foreach (var roleClaim in roleClaims)
                {

                    if (lstClaimsToken.Contains(roleClaim)) continue;

                    lstClaimsToken.Add(roleClaim);

                }

            }

            return lstClaimsToken;

        }

        /// <summary>
        /// GENERAR Y VERIFICA QUE SEA UN TOKEN VALIDO
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
     //GENERAR TOKEN PRINCIPAL
        private ClaimsPrincipal GenerarToken(string token)
        {


            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {

                var tokenComprobacion = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validarToken);

                if (!ComprobarAlgoritmoJWTBearer(validarToken))
                {

                    return null;

                }

                return tokenComprobacion;

            }
            catch
            {

                return null;

            }

        }

        /// <summary>
        /// COMPRUEBA QUE SEA UN TOKEN DE VERDAD Y QUE CUMPLA CON CIERTO ESTANDAR DE ENCRIPTAMIENTO
        /// </summary>
        /// <param name="validacion"></param>
        /// <returns></returns>
            private bool ComprobarAlgoritmoJWTBearer(SecurityToken validacion)
        {

            return (validacion is JwtSecurityToken jwtSecurityToken) &&
              jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
              StringComparison.InvariantCultureIgnoreCase);

        }

        #endregion

    }
}

