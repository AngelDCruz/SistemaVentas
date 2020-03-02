using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Repositorio.Contratos;

namespace SistemaVentas.Infraestructura.Repositorio
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

        public async Task<List<Guid>> ObtenerUsuariosRoleIdAsync(Guid idUsuario)
        {

            return await _context.UsuariosRoles
                .Where(x => x.UserId == idUsuario)
                .Select(x => x.RoleId)
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
