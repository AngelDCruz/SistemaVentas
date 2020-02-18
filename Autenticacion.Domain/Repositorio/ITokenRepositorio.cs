using System;
using System.Threading.Tasks;

using Autenticacion.Infraestructura.EntidadesConfiguracion;

namespace Autenticacion.Dominio.Repositorio
{
    public interface ITokenRepositorio
    {

        Task<TokenEntidad> CrearTokenAsync(TokenEntidad token);

        Task<bool> ActualizarTokenAsync(TokenEntidad token);

        Task<TokenEntidad> ObtenerTokenAsync(string token);

    }
}
