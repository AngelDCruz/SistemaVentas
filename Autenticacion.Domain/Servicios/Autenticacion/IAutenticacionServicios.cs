
using System.Threading.Tasks;
using Autenticacion.Aplicacion.DTO.Respuestas.v1;

namespace Autenticacion.Dominio.Servicios.Autenticacion
{
    public interface IAutenticacionServicios
    {

        Task<AutenticacionDTO> Login(string login, string password);

        Task<AutenticacionDTO> RefreshTokenAsync(string token, string refreshToken);

    }
}
