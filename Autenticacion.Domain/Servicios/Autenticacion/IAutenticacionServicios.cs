using Autenticacion.Dominio.DTO.Respuestas.v1;
using Autenticacion.Dominio.DTO.Solicitudes.v1;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Servicios.Autenticacion
{
    public interface IAutenticacionServicios
    {

        Task<AutenticacionDTO> Login(string login, string password);

        Task<AutenticacionDTO> RefreshTokenAsync(string token, string refreshToken);

    }
}
