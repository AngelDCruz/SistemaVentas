using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Servicios.TokenSession
{
    public class TokenSessionServicios : ITokenSessionServicios
    {

        private readonly ITokenSessionRepositorio _tokenSessionRepositorio;
        public TokenSessionServicios(ITokenSessionRepositorio tokenSessionRepositorio)
        {
            _tokenSessionRepositorio = tokenSessionRepositorio;
        }

        public Task<bool> CrearSessionToken(Guid id, string cadenaToken)
        {

            var tokenSession = new TokenSessionEntidad
            {
                Id = id,
                TokenSession = cadenaToken
            };

            return _tokenSessionRepositorio.CrearTokenSessionAsync(tokenSession);

        }

        public async Task<bool> EliminarSessionToken(TokenSessionEntidad token)
        {

            return await _tokenSessionRepositorio.EliminarTokenSessionAsync(token);

        }

        public async Task<TokenSessionEntidad> ObtenerTokenSessionAsync(Guid id)
        {
            return await _tokenSessionRepositorio.ObtenerTokenUsuarioIdAsync(id);
        }
    }
}
