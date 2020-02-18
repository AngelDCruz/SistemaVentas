using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Autenticacion.Dominio.DTO.Respuestas.v1;
using Autenticacion.Dominio.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio;
using Common.Paginacion;
using Microsoft.EntityFrameworkCore;
using Autenticacion.Dominio.Servicios.Roles;

namespace Autenticacion.Api.Servicios.Usuarios
{
    public partial class UsuariosServicios : IUsuariosServicios
    {

        private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly IUsuariosRolesRepositorio _usuariosRolesRepositorio;
        private readonly IRolesServicios _rolesServicicios;

        public UsuariosServicios(
            IUsuariosRepositorio usuariosRepositorio,
            IUsuariosRolesRepositorio usuariosRolesRepositorio,
            IRolesServicios rolesServicicios
        )
        {

            _usuariosRepositorio = usuariosRepositorio;
            _usuariosRolesRepositorio = usuariosRolesRepositorio;
            _rolesServicicios = rolesServicicios;
        }


        ////////////////////////////// USUARIOS ///////////////////////////////////
        
        /// <summary>
        /// LISTA DE USUARIOS CON CONDICIONES PARA PAGINAR, INCLUIR RELACIONES. 
        /// </summary>
        /// <param name="incluir"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public async Task<List<UsuariosDTO>> ObtenerUsuariosAsync(IncluirUsuariosDTO incluir, FiltroPagina filtro)
        {

            var lstUsuarios = _usuariosRepositorio.ObtenerUsuariosAsync()
                              .Include(x => x.UsuariosRoles)
                              .Where(x => x.Estatus != "Baj");

            if (lstUsuarios == null) return null;
            
            if (filtro != null)
            {

                var pagina = (filtro.Pagina - 1) * filtro.Limite;
                var limite = filtro.Limite;

                lstUsuarios = lstUsuarios
                              .Skip(pagina)
                              .Take(limite);

            }

            return await ObtenerUsuariosRelaciones(lstUsuarios, incluir);

        }

        /// <summary>
        /// OBTIENE UN USUARIO EN ESPECIFICO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UsuariosEntidad> ObtenerUsuarioIdAsync(Guid id)
        {

            return await _usuariosRepositorio.ObtenerUsuarioPorIdAsync(id);

        }

        public async Task<UsuariosDTO> ObtenerUsuarioIdRoleAsync(Guid id)
        {

            var usuario = await _usuariosRepositorio.ObtenerUsuarioPorIdAsync(id);

            return  await ObtenerUsuariosRoles(usuario);

        }

        public async Task<List<UsuariosDTO>> ObtenerUsuariosRoleIdAsync(Guid idRole)
        {


            List<UsuariosDTO> lstUsuariosDTO = new List<UsuariosDTO>();

            List<Guid> lstIdUsuarios = await _usuariosRolesRepositorio.ObtenerUsuariosRoleIdAsync(idRole);

            if (lstIdUsuarios != null)
            {

                foreach (var usuario in lstIdUsuarios)
                {

                    var usuarioRole = await ObtenerUsuarioIdAsync(usuario);

                    lstUsuariosDTO.Add(new UsuariosDTO
                    {
                        Id = usuarioRole.Id,
                        Usuario = usuarioRole.UserName,
                        Estatus = usuarioRole.Estatus,
                        FechaCreacion = usuarioRole.FechaCreacion
                    });

                }

            }

            return lstUsuariosDTO;

        }

        /// <summary>
        /// CREA UN USUARIO
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<Guid> CrearUsuarioAsync(UsuariosEntidad usuario)
        {

            usuario.FechaCreacion = DateTime.Now;

            return await _usuariosRepositorio.CrearUsuarioAsync(usuario);

        }

        /// <summary>
        /// ACTUALIZA UN USUARIO
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<bool> ActualizarUsuarioAsync(UsuariosEntidad usuario)
        {

            return await _usuariosRepositorio.ActualizarUsuarioAsync(usuario);

        }

        /// <summary>
        /// ELIMINA USUARIO
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public Task<bool> EliminarUsuarioAsync(UsuariosEntidad usuario)
        {

            return _usuariosRepositorio.EliminarUsuarioAsync(usuario);

        }
     
        /// <summary>
        /// OBTIENE UN USUARIO POR SU CORREO ELECTRONICO
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UsuariosEntidad> ObtenerUsuarioEmailAsync(string email)
        {

            return await _usuariosRepositorio.ObtenerUsuarioEmailAsync(email);

        }
    }
}
