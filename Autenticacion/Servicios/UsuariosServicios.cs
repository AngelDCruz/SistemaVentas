using Autenticacion.Api.DTO.Respuestas.v1;
using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio.Contratos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Api.Servicios
{
    public class UsuariosServicios : IUsuariosServicios
    {

        private readonly IUsuariosInformacionRepositorio _usuariosInformacionRepositorio;
        private readonly IUsuariosProcesoRepositorio _usuariosProcesoRepositorio,

        public UsuariosServicios(
            IUsuariosInformacionRepositorio usuariosInformacionRepositorio,
            IUsuariosProcesoRepositorio usuariosProcesoRepositorio
        )
        {

            _usuariosInformacionRepositorio = usuariosInformacionRepositorio;
            _usuariosProcesoRepositorio = usuariosProcesoRepositorio;
        }

        public async Task<bool> ActualizarUsuarioAsync(Usuarios solicitud)
        {

            return await _usuariosProcesoRepositorio.ActualizarUsuario(solicitud);
            
        }

        public async Task<Usuarios> CrearUsuarioAsync(Usuarios solicitud)
        {
           
            return await _usuariosProcesoRepositorio.CrearUsuario(solicitud);

        }

        public Task<bool> EliminarUsuarioAsync(Usuarios usuario)
        {

            return _usuariosProcesoRepositorio.EliminarUsuario(usuario);

        }

        public async Task<Usuarios> ObtenerUsuarioAsync(Guid id)
        {

            return await _usuariosInformacionRepositorio.ObtenerUsuarioPorId(id);

        }

        public async Task<List<Usuarios>> ObtenerUsuariosAsync()
        {

            return await _usuariosInformacionRepositorio.ObtenerUsuarios().ToListAsync();

        }
    }
}
