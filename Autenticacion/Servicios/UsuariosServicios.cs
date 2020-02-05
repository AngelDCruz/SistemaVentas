using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio.Contratos;
using System.Linq;

namespace Autenticacion.Api.Servicios
{
    public class UsuariosServicios : IUsuariosServicios
    {

        private readonly IUsuariosInfoRepositorio _usuariosInformacionRepositorio;
        private readonly IUsuariosProcesoRepositorio _usuariosProcesoRepositorio;

        public UsuariosServicios(
            IUsuariosInfoRepositorio usuariosInformacionRepositorio,
            IUsuariosProcesoRepositorio usuariosProcesoRepositorio,
            IRolesInfoRepositorio rolesInfoRepositorio
        )
        {

            _usuariosInformacionRepositorio = usuariosInformacionRepositorio;
            _usuariosProcesoRepositorio = usuariosProcesoRepositorio;
        }

        public async Task<bool> ActualizarUsuarioAsync(Usuarios usuario)
        {

            return await _usuariosProcesoRepositorio.ActualizarUsuarioAsync(usuario);
            
        }

        public async Task<Guid> CrearUsuarioAsync(Usuarios usuario)
        {

            usuario.FechaCreacion = DateTime.Now;

            return await _usuariosProcesoRepositorio.CrearUsuarioAsync(usuario);

        }

        public async Task<bool> CrearUsuarioRoleAsync(List<UsuariosRoles> lstUsuariosRoles)
        {

            return await _usuariosProcesoRepositorio.CrearUsuarioRoleAsync(lstUsuariosRoles);

        }

        public Task<bool> EliminarUsuarioAsync(Usuarios usuario)
        {

            return _usuariosProcesoRepositorio.EliminarUsuarioAsync(usuario);

        }


        public async Task<Usuarios> ObtenerUsuarioIdAsync(Guid id)
        {

            return await _usuariosInformacionRepositorio.ObtenerUsuarioPorIdAsync(id);

        }

        public async Task<List<Usuarios>> ObtenerUsuariosAsync()
        {

            return await _usuariosInformacionRepositorio
                .ObtenerUsuariosAsync()
                .ToListAsync();

        }

        public async Task<Usuarios> ObtenerUsuarioEmailAsync(string email)
        {

            return await _usuariosInformacionRepositorio.ObtenerUsuarioEmailAsync(email);

        }
    }
}
