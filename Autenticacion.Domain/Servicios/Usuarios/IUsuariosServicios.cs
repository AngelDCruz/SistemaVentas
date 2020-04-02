

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Autenticacion.Aplicacion.DTO.Respuestas.v1;
using Autenticacion.Aplicacion.DTO.Solicitudes.v1;

using Autenticacion.Dominio.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;

using Common.Paginacion;

namespace Autenticacion.Api.Servicios.Usuarios
{
    public interface IUsuariosServicios
    {

        /*
         * USUARIOS
         */
        Task<List<UsuariosDTO>> ObtenerUsuariosAsync(IncluirUsuariosDTO incluir, FiltroPagina filtro);

        Task<UsuariosEntidad> ObtenerUsuarioIdAsync(Guid id);

        Task<UsuariosEntidad> ObtenerUsuarioEmailAsync(string email);

        Task<UsuariosDTO> ObtenerUsuarioIdRoleAsync(Guid id);

        Task<List<UsuariosDTO>> ObtenerUsuariosRoleIdAsync(Guid idRole);

        Task<UsuariosEntidad> ObtenerUsuariosNombreAsync(string nombreUsuario);

        Task<UsuariosDTO> ObtenerUsuarioIdRelacionesAsync(Guid idUsuario, IncluirUsuariosDTO incluir);

        Task<Guid> CrearUsuarioAsync(UsuariosEntidad usuario);
        
        Task<bool> ActualizarUsuarioAsync(UsuariosEntidad usuario);

        Task<bool> EliminarUsuarioAsync(UsuariosEntidad usuario);


        /*
         * USUARIOS ROLES
         */

        Task<UsuariosRolesEntidad> ObtenerUsuarioRoleAsync(Guid idRole, Guid idUsuario);

        Task<bool> CrearUsuarioRoleAsync(CrearUsuarioRolesDTO usuarioRoleDTO);

        bool EliminarUsuarioRoleAsync(UsuariosRolesEntidad usuariosRoles);


    }
}
