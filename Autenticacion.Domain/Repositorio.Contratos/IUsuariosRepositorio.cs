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
        IQueryable<Usuarios> ObtenerUsuariosAsync(int limite, int pagina);

        Task<Usuarios> ObtenerUsuarioPorIdAsync(Guid id);

        Task<Usuarios> ObtenerUsuarioEmailAsync(string email);

        Task<Guid> CrearUsuarioAsync(Usuarios usuario);

        Task<bool> ActualizarUsuarioAsync(Usuarios usuario);

        Task<bool> EliminarUsuarioAsync(Usuarios usuarios);

    }
}
