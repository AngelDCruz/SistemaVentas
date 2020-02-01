
using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio.Contratos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Infraestructura.Repositorio
{
    public class UsuariosRepositorio : IUsuariosInformacionRepositorio, IUsuariosProcesoRepositorio
    {

        private readonly UserManager<Usuarios> _userManager;
        private readonly AutenticationDbContext _context;

        public UsuariosRepositorio(UserManager<Usuarios> userManager, AutenticationDbContext context)
        {

            _userManager = userManager;
            _context = context;

        }

        public IQueryable<Usuarios> ObtenerUsuarios()
        {

            return _userManager.Users.OfType<Usuarios>().AsQueryable();

        }

        public async Task<Usuarios> ObtenerUsuarioPorId(Guid id)
        {

            return await _userManager.FindByIdAsync(id.ToString());

        }


        public async Task<Usuarios> CrearUsuario(Usuarios usuario)
        {

            var respuesta = await _userManager.CreateAsync(usuario, usuario.PasswordHash);

            return respuesta.Succeeded ? usuario : null;


        }

        public async Task<bool> ActualizarUsuario(Usuarios usuario)
        {

            var respuesta = await _userManager.UpdateAsync(usuario);

            return respuesta.Succeeded ? true : false;

        }

        public async Task<bool> EliminarUsuario(Usuarios usuario)
        {

            _context.Users.Remove(usuario);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

    }
}
