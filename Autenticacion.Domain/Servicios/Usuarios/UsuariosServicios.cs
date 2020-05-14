using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


using Autenticacion.Dominio.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio;

using Common.Paginacion;

using Autenticacion.Dominio.Servicios.Roles;
using Autenticacion.Aplicacion.DTO.Respuestas.v1;
using SistemaVentas.Aplicacion.DTO.Solicitudes.v1;

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

            var lstUsuarios = _usuariosRepositorio.ObtenerUsuariosAsync();

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

        public  async Task<UsuariosDTO> ObtenerUsuarioIdRelacionesAsync(Guid idUsuario, IncluirUsuariosDTO incluir)
        {

            var lstUsuarios =  _usuariosRepositorio.ObtenerUsuariosAsync();

            if (incluir.Role) lstUsuarios = lstUsuarios.Include(x => x.UsuariosRoles);

            var usuarioEntidad = lstUsuarios.FirstOrDefault(x => x.Id == idUsuario);

            if (usuarioEntidad == null)
            {
                return new UsuariosDTO();
            }
            
            return await ObtenerUsuariosRelaciones(usuarioEntidad, incluir);

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

        public async Task<UsuariosEntidad> ObtenerUsuariosNombreAsync(string nombreUsuario)
        {

            return await _usuariosRepositorio.ObtenerUsuarioNombreAsync(nombreUsuario);

        }

        /// <summary>
        /// CREA UN USUARIO
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<Guid> CrearUsuarioAsync(UsuariosEntidad usuario)
        {

            usuario.Estatus = "Act";
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

        public async Task<bool> ActivarUsuarioAsync(Guid id)
        {

            var usuario = await _usuariosRepositorio.ObtenerUsuarioPorIdAsync(id);
            usuario.Estatus = "Act";

            return await _usuariosRepositorio.ActualizarUsuarioAsync(usuario);

        }

        public async Task<List<UsuariosEntidad>> FiltrarUsuariosAsync(FiltroUsuarioDTO filtro)
        {

            var lstUsuarios =  _usuariosRepositorio.ObtenerUsuariosAsync();

            if(!string.IsNullOrEmpty(filtro.Usuario))
            {

                lstUsuarios = lstUsuarios.Where(x => x.UserName.Contains(filtro.Usuario));

            }

            if(!string.IsNullOrEmpty(filtro.Email))
            {

                lstUsuarios = lstUsuarios.Where(x => x.Email.Contains(filtro.Email));

            }

            return await lstUsuarios.ToListAsync();

        }
    }
}
