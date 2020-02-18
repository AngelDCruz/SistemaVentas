using Autenticacion.Dominio.DTO.Respuestas.v1;
using Autenticacion.Dominio.DTO.Solicitudes.v1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Repositorio
{
    interface IAutenticacionRepositorio
    {

        Task<AutenticacionDTO> Login(string login, string password);

        Task<AutenticacionDTO> RefreshTokenAsync(string access_token, string refreshToken);

    }
}
