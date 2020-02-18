using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Infraestructura.Repositorio
{
    public class TokenSessionRepositorio : ITokenSessionRepositorio
    {

        private readonly AutenticacionDbContext _context;

        public TokenSessionRepositorio(AutenticacionDbContext context)
        {

            _context = context;

        }

        public async Task<bool> ComprobarTokenUsuarioIdAsync(Guid idUsuario)
        {
            return await _context.TokenSession.AnyAsync(x => x.Id == idUsuario);
        }

        public async Task<TokenSessionEntidad> ObtenerTokenUsuarioIdAsync(Guid id)
        {
            return await _context.TokenSession.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CrearTokenSessionAsync(TokenSessionEntidad token)
        {

            await _context.TokenSession.AddAsync(token);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> EliminarTokenSessionAsync(TokenSessionEntidad token)
        {

            _context.TokenSession.Remove(token);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        
    }
}
