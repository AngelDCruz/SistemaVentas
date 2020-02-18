
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio;

namespace Autenticacion.Infraestructura.Repositorio
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {

        private readonly UserManager<UsuariosEntidad> _userManager;

        private readonly AutenticacionDbContext _context;

        public UsuariosRepositorio(
            UserManager<UsuariosEntidad> userManager,
            AutenticacionDbContext context
         )
        {

            _userManager = userManager;
            _context = context;
        }

        /*
         * USUARIOS
         */
        public IQueryable<UsuariosEntidad> ObtenerUsuariosAsync() => 
            _context.Usuarios.Include(x => x.UsuariosRoles).AsQueryable();

        public async Task<UsuariosEntidad> ObtenerUsuarioPorIdAsync(Guid id)
        {

            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id &&
         x.Estatus != "Baj");

        }

        public async Task<UsuariosEntidad> ObtenerUsuarioEmailAsync(string email)
        {

            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && 
            x.Estatus != "Baj");

        }

        public async Task<IList<string>> ObtenerUsuariosRolesAsync(UsuariosEntidad usuario)
        {
            return  await _userManager.GetRolesAsync(usuario);
        }

        public async Task<Guid> CrearUsuarioAsync(UsuariosEntidad usuario)
        {

            usuario.UsuarioCreacion = _context.UsuarioAutenticado();

            var respuesta = await _userManager.CreateAsync(usuario, usuario.PasswordHash);

            return respuesta.Succeeded ? usuario.Id : Guid.Empty;

        }

 
        public async Task<bool> ActualizarUsuarioAsync(UsuariosEntidad usuario)
        {

            var actualizarUsuario = await ObtenerUsuarioPorIdAsync(usuario.Id);
            actualizarUsuario.PhoneNumber = usuario.PhoneNumber;
            actualizarUsuario.FechaModificacion = DateTime.Now;
            actualizarUsuario.SecurityStamp = Guid.NewGuid().ToString();
            actualizarUsuario.UsuarioModificacion = _context.UsuarioAutenticado();

            var respuesta = await _userManager.UpdateAsync(actualizarUsuario);

            return respuesta.Succeeded ? true : false;

        }

        public async Task<bool> EliminarUsuarioAsync(UsuariosEntidad usuario)
        {

            _context.Users.Remove(usuario);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> VerificarCredencialesAsync(UsuariosEntidad usuario, string password)
        {

            return await _userManager.CheckPasswordAsync(usuario, password);

        }

   
    }
}
