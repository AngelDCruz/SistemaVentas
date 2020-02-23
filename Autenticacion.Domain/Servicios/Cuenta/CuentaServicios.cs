using Autenticacion.Api.Servicios.Usuarios;
using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio;
using Common.Excepciones;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Servicios.Cuenta
{
    public class CuentaServicios : ICuentaServicios
    {

        private readonly ICuentaRepositorio _cuentaRepositorio;
        private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CuentaServicios(
            ICuentaRepositorio cuentaRepositorio,
            IUsuariosRepositorio usuariosRepositorio,
            IHttpContextAccessor httpContextAccessor 
        )
        {
            _cuentaRepositorio = cuentaRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        ///  CAMBIA IMAGEN DE PERFIL O AGREGA IMAGEN DE PERFIL
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="imagenURL"></param>
        /// <returns></returns>
        public async Task<string> CambiarImagenPerfilAsync(Guid idUsuario, string imagenURL)
        {
            return await _cuentaRepositorio.CambiarImagenPerfilAsync(idUsuario, imagenURL);
        }

        /// <summary>
        /// CAMBIO DE NOMBRE DEL PERFIL DEL USUARIO
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="nombreUsuario"></param>
        /// <returns></returns>
        public async Task<bool> CambiarNombrePerfilAsync(Guid idUsuario, string nombreUsuario)
        {

            var usuario = await _usuariosRepositorio.ObtenerUsuarioNombreAsync(nombreUsuario);

            if(usuario != null)
            {

                return false;

            }

            return await _cuentaRepositorio.CambiarNombrePerfilAsync(idUsuario, nombreUsuario);

        }

        /// <summary>
        /// RESTAURA EL PASSWORD DEL USUARIO AUTENTICADO PREVIAMENTE EN LA SESION
        /// </summary>
        /// <param name="passwordAnterior"></param>
        /// <param name="passwordNueva"></param>
        /// <returns></returns>
        public async Task<bool> RestaurarPasswordCuentaAsync(string passwordAnterior, string passwordNueva)
        {


            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {

                var IdUsuario = Guid.Parse(
                    _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value
                 );
                var usuario = await _usuariosRepositorio.ObtenerUsuarioPorIdAsync(IdUsuario);

                var verificarCuenta = await _usuariosRepositorio.VerificarCredencialesAsync(usuario, passwordAnterior);

                if (!verificarCuenta)
                {
                    throw new Excepcion("Usuario o contraseña incorrectas");
                }

                usuario.PasswordHash = passwordNueva;

                return await _cuentaRepositorio.RestaurarPasswordCuentaAsync(usuario);

            }

            return false;

        }
    }
}
