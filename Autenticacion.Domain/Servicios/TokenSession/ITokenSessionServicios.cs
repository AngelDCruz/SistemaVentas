using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Servicios.TokenSession
{
    public interface ITokenSessionServicios
    {

        Task<TokenSessionEntidad> ObtenerTokenSessionAsync(Guid id);

        Task<bool> CrearSessionToken(Guid id, string cadenaToken);

        Task<bool> EliminarSessionToken(TokenSessionEntidad token);

    }
}
