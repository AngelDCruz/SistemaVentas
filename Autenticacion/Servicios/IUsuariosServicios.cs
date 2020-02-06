using Autenticacion.Api.DTO.Respuestas.v1;
using Autenticacion.Api.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;
using Common.Paginacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Api.Servicios
{
    public interface IUsuariosServicios
    {

        /*
         * USUARIOS
         */
        Task<List<Usuarios>> ObtenerUsuariosAsync(int limite, int pagina);

        Task<Usuarios> ObtenerUsuarioIdAsync(Guid id);

        Task<Usuarios> ObtenerUsuarioEmailAsync(string email);

        Task<Guid> CrearUsuarioAsync(Usuarios usuario);
        
        Task<bool> ActualizarUsuarioAsync(Usuarios usuario);

        Task<bool> EliminarUsuarioAsync(Usuarios usuario);


        /*
         * USUARIOS ROLES
         */
        IEnumerable<UsuariosDTO> ObtenerUsuariosRoles(IncluirUsuariosDTO incluir, FiltroPagina filtro);

        Task<UsuariosRoles> ObtenerUsuarioRoleAsync(Guid idRole, Guid idUsuario);

        Task<bool> CrearUsuarioRoleAsync(List<UsuariosRoles> lstUsuariosRoles);

        bool EliminarUsuarioRoleAsync(UsuariosRoles usuariosRoles);

    }
}
