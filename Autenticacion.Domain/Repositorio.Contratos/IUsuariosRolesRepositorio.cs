
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.Dominio.Entidades;

namespace SistemaVentas.Dominio.Repositorio.Contratos
{
    public interface IUsuariosRolesRepositorio
    {

        /*
       * USUARIOS ROLES
       */
        Task<List<UsuariosRolesEntidad>> ObtenerUsuariosRoles();

        Task<UsuariosRolesEntidad> ObtenerUsuarioRoleAsync(Guid idRole, Guid idUsuario);

        Task<List<UsuariosRolesEntidad>> ObtenerUsuarioIdRolesAsync(Guid idUsuario);

        Task<List<Guid>> ObtenerUsuariosRoleIdAsync(Guid idUsuario);

        Task<bool> CrearUsuarioRoleAsync(List<UsuariosRolesEntidad> lstUsuariosRoles);

        bool EliminarUsuarioRole(UsuariosRolesEntidad usuariosRole);

    }
}
