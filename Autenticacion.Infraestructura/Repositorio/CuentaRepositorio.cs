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
    public class CuentaRepositorio : ICuentaRepositorio
    {

        private readonly AutenticacionDbContext _dbContext;

        public CuentaRepositorio(AutenticacionDbContext dbContext)
        {

            _dbContext = dbContext;

        }

        public async Task<string> CambiarImagenPerfilAsync(Guid idUsuario, string imagenURL)
        {

            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == idUsuario);
            usuario.ImagenPerfil = imagenURL;

            _dbContext.Entry(usuario).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync() > 0 ? usuario.ImagenPerfil : null;

        }

        public async Task<bool> CambiarNombrePerfilAsync(Guid idUsuario, string nombreUsuario)
        {

            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == idUsuario);
            usuario.UserName = nombreUsuario;

            _dbContext.Entry(usuario).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync() > 0 ? true : false;


        }

        public async Task<bool> RestaurarPasswordCuentaAsync(UsuariosEntidad usuario)
        {

            _dbContext.Entry(usuario).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync() > 0 ? true : false;

        }
    }
}
