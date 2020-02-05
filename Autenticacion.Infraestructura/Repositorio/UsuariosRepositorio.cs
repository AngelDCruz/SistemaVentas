
using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio.Contratos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Infraestructura.Repositorio
{
    public class UsuariosRepositorio : IUsuariosInfoRepositorio, IUsuariosProcesoRepositorio
    {

        private readonly UserManager<Usuarios> _userManager;
        private readonly AutenticacionDbContext _context;

        public UsuariosRepositorio(UserManager<Usuarios> userManager, AutenticacionDbContext context)
        {

            _userManager = userManager;
            _context = context;

        }

        public IQueryable<Usuarios> ObtenerUsuariosAsync() =>
            _context.Usuarios.Where(x => x.Estatus != "Baj").AsQueryable();

        public async Task<Usuarios> ObtenerUsuarioPorIdAsync(Guid id)
        {

            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id &&
         x.Estatus != "Baj");

        }

        public async Task<Usuarios> ObtenerUsuarioEmailAsync(string email)
        {

            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && 
            x.Estatus != "Baj");

        }

        public async Task<Guid> CrearUsuarioAsync(Usuarios usuario)
        {

            usuario.UsuarioCreacion = _context.UsuarioAutenticado();

            var respuesta = await _userManager.CreateAsync(usuario, usuario.PasswordHash);

            return respuesta.Succeeded ? usuario.Id : Guid.Empty;

        }

        public async Task<bool> CrearUsuarioRoleAsync(List<UsuariosRoles> lstUsuariosRoles)
        {

            await _context.UserRoles.AddRangeAsync(lstUsuariosRoles);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> ActualizarUsuarioAsync(Usuarios usuario)
        {

            var actualizarUsuario = await ObtenerUsuarioPorIdAsync(usuario.Id);
            actualizarUsuario.PhoneNumber = usuario.PhoneNumber;
            actualizarUsuario.FechaModificacion = DateTime.Now;
            actualizarUsuario.SecurityStamp = Guid.NewGuid().ToString();
            actualizarUsuario.UsuarioModificacion = _context.UsuarioAutenticado();

            var respuesta = await _userManager.UpdateAsync(actualizarUsuario);

            return respuesta.Succeeded ? true : false;

        }

        public async Task<bool> EliminarUsuarioAsync(Usuarios usuario)
        {

            _context.Users.Remove(usuario);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

    }
}
