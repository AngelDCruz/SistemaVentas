using System;
using System.Linq;
using System.Threading.Tasks;
using Autenticacion.Dominio.Repositorio;
using Autenticacion.Infraestructura.EntidadesConfiguracion;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Infraestructura.Repositorio
{
   public  class TokenRepositorio : ITokenRepositorio
    {

        private readonly AppDbContext _context;

        public TokenRepositorio(AppDbContext context)
        {

            _context = context;

        }

        public async Task<TokenEntidad> ObtenerTokenAsync(string token)
        {

            return await _context.Token.FirstOrDefaultAsync(x => x.Token == token);

        }

        public  async Task<TokenEntidad> CrearTokenAsync(TokenEntidad token)
        {

            await _context.Token.AddAsync(token);

           return await _context.SaveChangesAsync() > 0 ? token : null;

        }

        public async Task<bool> ActualizarTokenAsync(TokenEntidad token)
        {

            _context.Token.Update(token);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }


    }
}
