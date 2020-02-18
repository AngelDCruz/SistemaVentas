using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autenticacion.Dominio.DTO.Respuestas.v1;
using Autenticacion.Dominio.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;


namespace Autenticacion.Api.Servicios.Usuarios
{
    public partial class UsuariosServicios
    {

        ///////////////////////////// USUARIOS ROLES /////////////////////////////////

        /// <summary>
        /// LISTA DE USUARIOS CON RELACION DE ROLES
        /// </summary>
        /// <returns></returns>
        public async Task<List<UsuariosDTO>> ObtenerUsuariosRoles()
        {
            var lstUsuarios = _usuariosRepositorio.ObtenerUsuariosAsync();

            return await ObtenerUsuariosRelaciones(lstUsuarios);

        }

        /// <summary>
        /// OBTIENE LISTA DE USUARIO ROLES
        /// </summary>
        /// <param name="idRole"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public async Task<UsuariosRolesEntidad> ObtenerUsuarioRoleAsync(Guid idRole, Guid idUsuario)
        {

            return await _usuariosRolesRepositorio.ObtenerUsuarioRoleAsync(idRole, idUsuario);

        }

        /// <summary>
        /// CREA UN ROL PARA UN USUARIO
        /// </summary>
        /// <param name="usuarioRolesDTO"></param>
        /// <returns></returns>
        public async Task<bool> CrearUsuarioRoleAsync(CrearUsuarioRolesDTO usuarioRolesDTO)
        {

            var usuarioRoles = AsignarRolesUsuario(usuarioRolesDTO);

            if (usuarioRoles != null)
            {

                return await _usuariosRolesRepositorio.CrearUsuarioRoleAsync(usuarioRoles);

            }

            return false;
        }

        /// <summary>
        /// ELIMINA UN ROL DEL USUARIO
        /// </summary>
        /// <param name="usuariosRoles"></param>
        /// <returns></returns>
        public bool EliminarUsuarioRoleAsync(UsuariosRolesEntidad usuariosRoles)
        {

            return _usuariosRolesRepositorio.EliminarUsuarioRole(usuariosRoles);

        }

    }
}
