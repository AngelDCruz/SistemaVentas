using Autenticacion.Api.DTO.Respuestas.v1;
using Autenticacion.Api.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio.Contratos;
using AutoMapper;
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
        private readonly IUsuariosProcesoRepositorio _usuariosProcesoRepositorio;

        public UsuariosServicios(
            IUsuariosInformacionRepositorio usuariosInformacionRepositorio,
            IUsuariosProcesoRepositorio usuariosProcesoRepositorio
        )
        {

            _usuariosInformacionRepositorio = usuariosInformacionRepositorio;
            _usuariosProcesoRepositorio = usuariosProcesoRepositorio;
        }

        public async Task<bool> ActualizarUsuarioAsync(Usuarios usuario)
        {

            var actualizar = await ObtenerUsuarioIdAsync(usuario.Id);

            actualizar.PhoneNumber = usuario.PhoneNumber;
            actualizar.FechaModificacion = DateTime.Now;
            actualizar.SecurityStamp = Guid.NewGuid().ToString();

            return await _usuariosProcesoRepositorio.ActualizarUsuario(usuario);
            
        }

        public async Task<Guid> CrearUsuarioAsync(Usuarios usuario)
        {

            usuario.FechaCreacion = DateTime.Now;

            return await _usuariosProcesoRepositorio.CrearUsuario(usuario);

        }

        public Task<bool> EliminarUsuarioAsync(Guid id)
        {

            return _usuariosProcesoRepositorio.EliminarUsuario(id);

        }


        public async Task<Usuarios> ObtenerUsuarioIdAsync(Guid id)
        {

            return await _usuariosInformacionRepositorio.ObtenerUsuarioPorIdAsync(id);

        }

        public async Task<List<Usuarios>> ObtenerUsuariosAsync()
        {

            return await _usuariosInformacionRepositorio.ObtenerUsuarios().ToListAsync();

        }

        public async Task<Usuarios> ObtenerUsuarioEmailAsync(string email)
        {

            return await _usuariosInformacionRepositorio.ObtenerUsuarioEmailAsync(email);

        }
    }
}
