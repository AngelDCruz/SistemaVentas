using Autenticacion.Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Repositorio
{
    public interface ICuentaRepositorio
    {

        Task<bool> RestaurarPasswordCuentaAsync(UsuariosEntidad usuario);

        Task<string> CambiarImagenPerfilAsync(Guid idUsuario, string imagenURL);

        Task<bool> CambiarNombrePerfilAsync(Guid idUsuario, string nombreUsuario);

    }
}
