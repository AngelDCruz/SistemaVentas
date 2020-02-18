
using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Repositorio
{
    public interface ITokenSessionRepositorio
    {


        Task<TokenSessionEntidad> ObtenerTokenUsuarioIdAsync(Guid id);

        Task<bool> ComprobarTokenUsuarioIdAsync(Guid idUsuario);

        Task<bool> CrearTokenSessionAsync(TokenSessionEntidad token);

        Task<bool> EliminarTokenSessionAsync(TokenSessionEntidad token);

    }
}
