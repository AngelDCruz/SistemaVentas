
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
    public class UsuariosRepositorio : IUsuariosInformacionRepositorio, IUsuariosProcesoRepositorio
    {

        private readonly UserManager<Usuarios> _userManager;
        private readonly AutenticacionDbContext _context;

        public UsuariosRepositorio(UserManager<Usuarios> userManager, AutenticacionDbContext context)
        {

            _userManager = userManager;
            _context = context;

        }

        public IQueryable<Usuarios> ObtenerUsuarios()
        {

            return _userManager.Users.OfType<Usuarios>().AsQueryable();

        }

        public async Task<Usuarios> ObtenerUsuarioPorIdAsync(Guid id)
        {

            return await _userManager.FindByIdAsync(id.ToString());

        }

        public async Task<Usuarios> ObtenerUsuarioEmailAsync(string email)
        {

            return await _userManager.FindByEmailAsync(email);

        }

        public async Task<Guid> CrearUsuario(Usuarios usuario)
        {

            usuario.UsuarioCreacion = _context.UsuarioAutenticado();

            var respuesta = await _userManager.CreateAsync(usuario, usuario.PasswordHash);

            return respuesta.Succeeded ? usuario.Id : Guid.Empty;

        }

        public async Task<bool> ActualizarUsuario(Usuarios usuario)
        {

            usuario.UsuarioModificacion = _context.UsuarioAutenticado();

            var respuesta = await _userManager.UpdateAsync(usuario);

            return respuesta.Succeeded ? true : false;

        }

        public async Task<bool> EliminarUsuario(Guid id)
        {

            _context.Users.Remove(await _context.Users.FirstOrDefaultAsync(x => x.Id == id));

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

    }
}
