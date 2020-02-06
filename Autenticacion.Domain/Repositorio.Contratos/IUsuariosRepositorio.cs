using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Repositorio.Contratos
{
    public interface IUsuariosRepositorio
    {

        /*
         * INFORMACION DEL USUARIO
         */
        IQueryable<UsuariosEntidad> ObtenerUsuariosAsync();

        Task<UsuariosEntidad> ObtenerUsuarioPorIdAsync(Guid id);

        Task<UsuariosEntidad> ObtenerUsuarioEmailAsync(string email);

        Task<Guid> CrearUsuarioAsync(UsuariosEntidad usuario);

        Task<bool> ActualizarUsuarioAsync(UsuariosEntidad usuario);

        Task<bool> EliminarUsuarioAsync(UsuariosEntidad usuarios);

    }
}
