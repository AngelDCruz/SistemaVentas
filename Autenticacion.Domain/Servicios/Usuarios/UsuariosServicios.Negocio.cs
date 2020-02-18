using Autenticacion.Dominio.DTO.Respuestas.v1;
using Autenticacion.Dominio.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Api.Servicios.Usuarios
{
    public partial class UsuariosServicios
    {



        //LOGICA DE NEGOCIOS 


        /// <summary>
        /// OBTIENE UN USUARIO CON SUS RELACIONES, DEPENDIENDO SI DESEA UTILIZARLAS O NO.
        /// </summary>
        /// <param name="incluir"></param>
        /// <param name="lstUsuarios"></param>
        /// <returns>LISTA DE USUARIOS MAPEADOS CON DTO</returns>
        private async Task<List<UsuariosDTO>> ObtenerUsuariosRelaciones(
                                                          IQueryable<UsuariosEntidad> lstUsuarios,
                                                          IncluirUsuariosDTO incluir = null)
        {


            Guid UsuarioActual = Guid.Empty;
            List<UsuariosDTO> lstUsuariosDTO = new List<UsuariosDTO>();

            if (lstUsuarios != null)
            {

       
                foreach (var usuario in lstUsuarios)
                {

                    var usuarioDTO = new UsuariosDTO
                    {
                        Id = usuario.Id,
                        Usuario = usuario.UserName,
                        Email = usuario.Email,
                        Telefono = usuario.PhoneNumber,
                        FechaCreacion = usuario.FechaCreacion,
                        Estatus = usuario.Estatus
                    };

                    if (incluir.Role)
                    {

                        usuarioDTO.Roles = await ObtenerRolesUsuarioAsync(usuario);

                    }

                    lstUsuariosDTO.Add(usuarioDTO);

                }
            }

            return lstUsuariosDTO;

        }

        /// <summary>
        /// OBTIENE UN USUARIO CON SUS RELACIONES, DEPENDIENDO SI DESEA UTILIZARLAS O NO.
        /// </summary>
        /// <param name="incluir"></param>
        /// <param name="lstUsuarios"></param>
        /// <returns>LISTA DE USUARIOS MAPEADOS CON DTO</returns>
        private async Task<UsuariosDTO> ObtenerUsuariosRoles(UsuariosEntidad usuario)
        {


            Guid UsuarioActual = Guid.Empty;
            UsuariosDTO UsuariosRolesDTO = null;

            if (usuario != null)
            {

                    var usuarioDTO = new UsuariosDTO
                    {
                        Id = usuario.Id,
                        Usuario = usuario.UserName,
                        Email = usuario.Email,
                        Telefono = usuario.PhoneNumber,
                        FechaCreacion = usuario.FechaCreacion,
                        Estatus = usuario.Estatus
                    };


                    UsuariosRolesDTO.Roles = await ObtenerRolesUsuarioAsync(usuario);
            }

            return UsuariosRolesDTO;

        }

        /// <summary>
        /// CREA UNA LISTA DE USUARIOS ROLES PARA ALMACENAR EN LA BASE DE DATOS
        /// </summary>
        /// <param name="usuarioRolesDTO"></param>
        /// <returns>LISTA DE USUARIOS ROLES</returns>
        private List<UsuariosRolesEntidad> AsignarRolesUsuario(CrearUsuarioRolesDTO usuarioRolesDTO)
        {

            List<UsuariosRolesEntidad> lstUsuariosRoles = new List<UsuariosRolesEntidad>();


            if (usuarioRolesDTO.Roles != null)
            {

                foreach (var role in usuarioRolesDTO.Roles)
                {

                    lstUsuariosRoles.Add(
                        new UsuariosRolesEntidad
                        {
                            RoleId = role,
                            UserId = usuarioRolesDTO.IdUsuario
                        }
                    );

                }

            }

            return lstUsuariosRoles;

        }

        private async Task<List<RolesDTO>> ObtenerRolesUsuarioAsync(UsuariosEntidad usuario)
        {

            List<RolesDTO> lstRolesDTO = new List<RolesDTO>();

            var lstRolesId = await _usuariosRolesRepositorio.ObtenerUsuariosRoleIdAsync(usuario.Id);

            if (lstRolesId != null)
            {

                foreach (var idRole in lstRolesId)
                {

                    var role = await _rolesServicicios.ObtenerRoleIdAsync(idRole);
                    var roleActualUsuarios = await _usuariosRolesRepositorio.ObtenerUsuarioRoleAsync(idRole, usuario.Id);

                    lstRolesDTO.Add(new RolesDTO
                    {
                        Id = role.Id,
                        Nombre = role.Name,
                        Estatus = roleActualUsuarios.Estatus,
                        FechaCreacion = roleActualUsuarios.FechaCreacion
                    });

                }

            }

            return lstRolesDTO;
        }

    }
}
