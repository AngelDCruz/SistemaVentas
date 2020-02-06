

using Autenticacion.Dominio.DTO.Respuestas.v1;
using Autenticacion.Dominio.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private IEnumerable<UsuariosDTO> ObtenerUsuariosRelaciones(IncluirUsuariosDTO incluir,
                                                          IQueryable<UsuariosEntidad> lstUsuarios)
        {


            Guid UsuarioActual = Guid.Empty;

            if (lstUsuarios != null)
            {

                UsuariosDTO UsuariosRolesDTO = null;

                foreach (var usuario in lstUsuarios)
                {

                    UsuariosRolesDTO = new UsuariosDTO
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

                        foreach (var prop in _usuariosRolesRepositorio.ObtenerUsuarioIdRolesAsync(usuario.Id).Result)
                        {


                            if (UsuarioActual == prop.UserId) continue;

                            UsuariosRolesDTO.Roles = ObtenerRolesDTO(prop);

                            UsuarioActual = prop.UserId;

                        }

                    }

                    yield return UsuariosRolesDTO;

                }
            }

        }


        /// <summary>
        /// LISTA DE USUARIOS CON RELACION DE ROLES
        /// </summary>
        /// <param name="lstUsuarios"></param>
        /// <returns>LISTA DE USUARIOS CON ROLES</returns>
        private UsuariosDTO UsuarioRoles(UsuariosEntidad usuario)
        {

            UsuariosDTO UsuariosRolesDTO = null;

            Guid UsuarioActual = Guid.Empty;

            if (usuario != null)
            {



                UsuariosRolesDTO = new UsuariosDTO
                {
                   Id = usuario.Id,
                   Usuario = usuario.UserName,
                   Email = usuario.Email,
                   Telefono = usuario.PhoneNumber,
                   FechaCreacion = usuario.FechaCreacion,
                   Estatus = usuario.Estatus
                 };


                 foreach (var prop in _usuariosRolesRepositorio.ObtenerUsuarioIdRolesAsync(usuario.Id).Result)
                 {


                    if (UsuarioActual == prop.UserId) continue;

                        UsuariosRolesDTO.Roles = ObtenerRolesDTO(prop);

                        UsuarioActual = prop.UserId;

                    }


                }
            
            return UsuariosRolesDTO;

        }

        /// <summary>
        /// LISTA DE USUARIOS CON RELACION DE ROLES
        /// </summary>
        /// <param name="lstUsuarios"></param>
        /// <returns>LISTA DE USUARIOS CON ROLES</returns>
        private IEnumerable<UsuariosRolesDTO> UsuariosRoles(IQueryable<UsuariosEntidad> lstUsuarios)
        {


            Guid UsuarioActual = Guid.Empty;

            if (lstUsuarios != null)
            {

                UsuariosRolesDTO UsuariosRolesDTO = null;

                foreach (var usuario in lstUsuarios)
                {

                    UsuariosRolesDTO = new UsuariosRolesDTO
                    {
                        Id = usuario.Id,
                        Usuario = usuario.UserName,
                        Email = usuario.Email,
                        Telefono = usuario.PhoneNumber,
                        FechaCreacion = usuario.FechaCreacion,
                        Estatus = usuario.Estatus
                    };


                    foreach (var prop in _usuariosRolesRepositorio.ObtenerUsuarioIdRolesAsync(usuario.Id).Result)
                    {


                        if (UsuarioActual == prop.UserId) continue;

                        UsuariosRolesDTO.Roles = ObtenerRolesDTO(prop);

                        UsuarioActual = prop.UserId;

                    }

                    yield return UsuariosRolesDTO;

                }
            }

        }

        /// <summary>
        /// MAPEA UNA LISTA DE ROLES A DTO
        /// </summary>
        /// <param name="usuariosRoles"></param>
        /// <returns>LISTA DE ROLES DTO</returns>
        private IEnumerable<RolesDTO> ObtenerRolesDTO(UsuariosRolesEntidad usuariosRoles)
        {


            foreach (var roles in usuariosRoles.Usuarios.UsuariosRoles)
            {

                if (roles != null)
                {
                    yield return new RolesDTO()
                    {
                        Id = roles.RoleId,
                        Nombre = roles.Roles.Name,
                        FechaCreacion = roles.FechaCreacion,
                        Estatus = roles.Roles.Estatus
                    };
                }
            }
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
                            UserId = usuarioRolesDTO.IdUsuario,
                            RolesId = role,
                            UsuariosId = usuarioRolesDTO.IdUsuario,
                        }
                    );

                }

            }

            return lstUsuariosRoles;

        }

    }
}
