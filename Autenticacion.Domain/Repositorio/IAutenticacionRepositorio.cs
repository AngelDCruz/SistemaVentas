
using System.Threading.Tasks;

using Autenticacion.Aplicacion.DTO.Respuestas.v1;

namespace Autenticacion.Dominio.Repositorio
{
    interface IAutenticacionRepositorio
    {

        Task<AutenticacionDTO> Login(string login, string password);

        Task<AutenticacionDTO> RefreshTokenAsync(string access_token, string refreshToken);

    }
}
