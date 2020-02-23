using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Repositorio
{
    public interface IUsuariosRepositorio
    {

        /*
         * INFORMACION DEL USUARIO
         */
        IQueryable<UsuariosEntidad> ObtenerUsuariosAsync();

        Task<UsuariosEntidad> ObtenerUsuarioPorIdAsync(Guid id);

        Task<UsuariosEntidad> ObtenerUsuarioEmailAsync(string email);

        Task<UsuariosEntidad> ObtenerUsuarioNombreAsync(string nombreUsuario);

        Task<IList<string>> ObtenerUsuariosRolesAsync(UsuariosEntidad usuario);

        Task<Guid> CrearUsuarioAsync(UsuariosEntidad usuario);

        Task<bool> ActualizarUsuarioAsync(UsuariosEntidad usuario);

        Task<bool> EliminarUsuarioAsync(UsuariosEntidad usuarios);

        Task<bool> VerificarCredencialesAsync(UsuariosEntidad usuario, string password);

        Task<bool> ActualizarDatosPersonalesUsuarioAsync(UsuariosEntidad usuario);

    }
}
