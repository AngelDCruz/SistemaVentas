using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Infraestructura.Repositorio
{

    /*
     * USUARIOS ROLES
     */
    public class UsuariosRolesRepositorio : IUsuariosRolesRepositorio
    {

        private readonly AutenticacionDbContext _context;

        public UsuariosRolesRepositorio(AutenticacionDbContext context)
        {

            _context = context;

        }

        public async Task<List<UsuariosRolesEntidad>> ObtenerUsuariosRoles()
        {

            return await _context.UsuariosRoles.
                Include(x => x.Usuarios)
                .Include(y => y.Roles)
                .ToListAsync();

        }

        public async Task<UsuariosRolesEntidad> ObtenerUsuarioRoleAsync(Guid idRole, Guid idUsuario)
        {

            return await _context.UsuariosRoles
                .FirstOrDefaultAsync(x => x.RoleId == idRole &&
                                     x.UserId == idUsuario && 
                                     x.Estatus != "Baj"
                                    );

        }

        public async Task<List<UsuariosRolesEntidad>> ObtenerUsuarioIdRolesAsync(Guid idUsuario)
        {
            return await _context.UsuariosRoles
                .Include(x => x.Usuarios)
                .Include(y => y.Roles)
                .Where(z => z.UserId == idUsuario)
                .ToListAsync();
        }

        public async Task<List<Guid>> ObtenerUsuariosRoleIdAsync(Guid idRole)
        {

            return await _context.UsuariosRoles
                .Where(x => x.RolesId == idRole)
                .Select(x => x.UserId)
                .Distinct()
                .ToListAsync();

        }

        public async Task<bool> CrearUsuarioRoleAsync(List<UsuariosRolesEntidad> lstUsuariosRoles)
        {

            await _context.UsuariosRoles.AddRangeAsync(lstUsuariosRoles);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public bool EliminarUsuarioRole(UsuariosRolesEntidad usuariosRoles)
        {

            _context.UsuariosRoles.Remove(usuariosRoles);

            return _context.SaveChanges() > 0 ? true : false;

        }

       
    }
}
