﻿using System.Linq;
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
                              .Include(x => x.DatosPersonales)
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

        public  async Task<UsuariosDTO> ObtenerUsuarioIdRelacionesAsync(Guid idUsuario, IncluirUsuariosDTO incluir)
        {

            var lstUsuarios =  _usuariosRepositorio.ObtenerUsuariosAsync();

            if (incluir.Datos) lstUsuarios = lstUsuarios.Include(x => x.DatosPersonales);

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

        /// <summary>
        /// ACTUALIZA LOS DATOS PERSONALES DE UN USUARIO EN ESPECIFICO
        /// </summary>
        /// <param name="datosPersonales"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public async Task<bool> ActualizarDatosPersonalesUsuarioAsync(DatosPersonalesEntidad datosPersonales, Guid idUsuario)
        {

            var usuario = await _usuariosRepositorio.ObtenerUsuarioPorIdAsync(idUsuario);
            usuario.DatosPersonales.Nombre = datosPersonales.Nombre;
            usuario.DatosPersonales.ApellidoPaterno = datosPersonales.ApellidoPaterno;
            usuario.DatosPersonales.ApellidoMaterno = datosPersonales.ApellidoMaterno;
            usuario.DatosPersonales.Pais = datosPersonales.Pais;
            usuario.DatosPersonales.Ciudad = datosPersonales.Ciudad;
            usuario.DatosPersonales.Calle = datosPersonales.Calle;
            usuario.DatosPersonales.Nombre = datosPersonales.Nombre;
            usuario.DatosPersonales.Telefono = datosPersonales.Telefono;

            return await _usuariosRepositorio.ActualizarDatosPersonalesUsuarioAsync(usuario);

        }
    }
}
