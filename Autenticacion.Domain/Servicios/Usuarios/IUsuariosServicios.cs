
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SistemaVentas.Dominio.Entidades;
using SistemaVentas.DTO.Respuestas.v1;
using SistemaVentas.DTO.Solicitudes.v1;

using Common.Paginacion;

namespace SistemaVentas.Api.Servicios.Usuarios
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

        Task<Guid> CrearUsuarioAsync(UsuariosEntidad usuario);

        Task<bool> ActualizarUsuarioAsync(UsuariosEntidad usuario);

        Task<bool> EliminarUsuarioAsync(UsuariosEntidad usuario);


        /*
         * USUARIOS ROLES
         */
        //Task<List<UsuariosRolesDTO>> ObtenerUsuariosRoles();

        Task<UsuariosRolesEntidad> ObtenerUsuarioRoleAsync(Guid idRole, Guid idUsuario);

        Task<bool> CrearUsuarioRoleAsync(CrearUsuarioRolesDTO usuarioRoleDTO);

        bool EliminarUsuarioRoleAsync(UsuariosRolesEntidad usuariosRoles);

    }
}
