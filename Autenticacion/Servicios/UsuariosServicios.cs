using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio.Contratos;
using System.Linq;
using Autenticacion.Api.DTO.Respuestas.v1;
using Autenticacion.Api.DTO.Solicitudes.v1;
using Common.Paginacion;

namespace Autenticacion.Api.Servicios
{
    public class UsuariosServicios : IUsuariosServicios
    {

        private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly IUsuariosRolesRepositorio _usuariosRolesRepositorio;

        public UsuariosServicios(
            IUsuariosRepositorio usuariosRepositorio,
            IUsuariosRolesRepositorio usuariosRolesRepositorio
        )
        {

            _usuariosRepositorio = usuariosRepositorio;
            _usuariosRolesRepositorio = usuariosRolesRepositorio;
        }
      
        /*
         * USUARIOS
         */
        public async Task<List<Usuarios>> ObtenerUsuariosAsync(int limite, int pagina)
        {

            return await _usuariosRepositorio
                .ObtenerUsuariosAsync(limite, pagina).ToListAsync();

        }

        public async Task<Usuarios> ObtenerUsuarioIdAsync(Guid id)
        {

            return await _usuariosRepositorio.ObtenerUsuarioPorIdAsync(id);

        }

        public async Task<Guid> CrearUsuarioAsync(Usuarios usuario)
        {

            usuario.FechaCreacion = DateTime.Now;

            return await _usuariosRepositorio.CrearUsuarioAsync(usuario);

        }

        public async Task<bool> ActualizarUsuarioAsync(Usuarios usuario)
        {

            return await _usuariosRepositorio.ActualizarUsuarioAsync(usuario);

        }

        public Task<bool> EliminarUsuarioAsync(Usuarios usuario)
        {

            return _usuariosRepositorio.EliminarUsuarioAsync(usuario);

        }
     
        public async Task<Usuarios> ObtenerUsuarioEmailAsync(string email)
        {

            return await _usuariosRepositorio.ObtenerUsuarioEmailAsync(email);

        }


        /*
         * USUARIOS ROLES
         */
        public IEnumerable<UsuariosDTO> ObtenerUsuariosRoles(IncluirUsuariosDTO incluir, FiltroPagina filtro)
        {

            Guid UsuarioActual = Guid.Empty;

            var lstUsuarios = _usuariosRepositorio.ObtenerUsuariosAsync(filtro.Limite, filtro.Pagina); 

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

        public async Task<UsuariosRoles> ObtenerUsuarioRoleAsync(Guid idRole, Guid idUsuario)
        {

            return await _usuariosRolesRepositorio.ObtenerUsuarioRoleAsync(idRole, idUsuario);

        }

        public async Task<bool> CrearUsuarioRoleAsync(List<UsuariosRoles> lstUsuariosRoles)
        {

            return await _usuariosRolesRepositorio.CrearUsuarioRoleAsync(lstUsuariosRoles);

        }

        public bool EliminarUsuarioRoleAsync(UsuariosRoles usuariosRoles)
        {

            return _usuariosRolesRepositorio.EliminarUsuarioRole(usuariosRoles);

        }



        private IEnumerable<RolesDTO> ObtenerRolesDTO(UsuariosRoles usuariosRoles)
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

    }
}
