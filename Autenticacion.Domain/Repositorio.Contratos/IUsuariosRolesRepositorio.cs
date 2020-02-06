using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Repositorio.Contratos
{
    public interface IUsuariosRolesRepositorio
    {

        /*
       * USUARIOS ROLES
       */
        Task<List<UsuariosRoles>> ObtenerUsuariosRoles();

        Task<UsuariosRoles> ObtenerUsuarioRoleAsync(Guid idRole, Guid idUsuario);

        Task<List<UsuariosRoles>> ObtenerUsuarioIdRolesAsync(Guid idUsuario);

        Task<bool> CrearUsuarioRoleAsync(List<UsuariosRoles> lstUsuariosRoles);

        bool EliminarUsuarioRole(UsuariosRoles usuariosRole);

    }
}
