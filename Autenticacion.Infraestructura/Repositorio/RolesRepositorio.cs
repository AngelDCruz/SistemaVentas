using Autenticacion.Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autenticacion.Dominio.Repositorio;
using System.Security.Claims;

namespace Autenticacion.Infraestructura.Repositorio
{
    public class RolesRepositorio : IRolesRepositorio
    {

        private readonly RoleManager<RolesEntidad> _roleManager;
        private readonly AppDbContext _autenticacionDbContext;

        public RolesRepositorio(
            RoleManager<RolesEntidad> roleManager,
            AppDbContext autenticacionDbContext
         )
        {
            
            _roleManager = roleManager;
            _autenticacionDbContext = autenticacionDbContext;
        }

        public async Task<List<RolesEntidad>> ObtenerRolesAsync()
        {

            return await _autenticacionDbContext.Roles.OfType<RolesEntidad>()
                .Where(x => x.Estatus != "Baj").ToListAsync();

        }

        public async Task<RolesEntidad> ObtenerRoleIdAsync(Guid id)
        {

            return await _autenticacionDbContext.
                Roles
                .Include(x => x.UsuariosRoles)
                .FirstOrDefaultAsync(x => x.Id == id && x.Estatus != "Baj");

        }

        public async Task<RolesEntidad> ObtenerRoleNombreAsync(string nombre)
        {

            return await _autenticacionDbContext.
                Roles.FirstOrDefaultAsync(x => x.Name == nombre && x.Estatus != "Baj");

        }

        public async Task<IEnumerable<Claim>> ObtenerRoleClaimAsync(RolesEntidad roles)
        {

            return await _roleManager.GetClaimsAsync(roles);

        }

        public async Task<RolesEntidad> CrearRoleAsync(RolesEntidad role)
        {

            var respuesta = await _roleManager.CreateAsync(role);

            return respuesta.Succeeded ? role : null;

        }

        public async Task<bool> ActualizarRoleAsync(RolesEntidad role)
        {

            var actualizarRole = await ObtenerRoleIdAsync(role.Id);
            actualizarRole.Name = role.Name;

            var respuesta = await _roleManager.UpdateAsync(actualizarRole);

            return respuesta.Succeeded ? true : false;

        }

        public async Task<bool> EliminarRoleIdAsync(RolesEntidad role)
        {

            var respuesta = await _roleManager.DeleteAsync(role);

            return respuesta.Succeeded ? true : false;

        }

    }
}
