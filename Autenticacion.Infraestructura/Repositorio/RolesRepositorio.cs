using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio.Contratos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Infraestructura.Repositorio
{
    public class RolesRepositorio : IRolesRepositorio
    {

        private readonly RoleManager<Roles> _roleManager;
        private readonly AutenticacionDbContext _autenticacionDbContext;

        public RolesRepositorio(
            RoleManager<Roles> roleManager,
            AutenticacionDbContext autenticacionDbContext
         )
        {
            
            _roleManager = roleManager;
            _autenticacionDbContext = autenticacionDbContext;
        }

        public async Task<List<Roles>> ObtenerRolesAsync()
        {

            return await _autenticacionDbContext.Roles.OfType<Roles>()
                .Where(x => x.Estatus != "Baj").ToListAsync();

        }

        public async Task<Roles> ObtenerRoleIdAsync(Guid id)
        {

            return await _autenticacionDbContext.
                Roles.FirstOrDefaultAsync(x => x.Id == id && x.Estatus != "Baj");

        }

        public async Task<Roles> ObtenerRoleNombreAsync(string nombre)
        {

            return await _autenticacionDbContext.
                Roles.FirstOrDefaultAsync(x => x.Name == nombre && x.Estatus != "Baj");

        }

        public async Task<Roles> CrearRoleAsync(Roles role)
        {

            var respuesta = await _roleManager.CreateAsync(role);

            return respuesta.Succeeded ? role : null;

        }

        public async Task<bool> ActualizarRoleAsync(Roles role)
        {

            var actualizarRole = await ObtenerRoleIdAsync(role.Id);
            actualizarRole.Name = role.Name;

            var respuesta = await _roleManager.UpdateAsync(actualizarRole);

            return respuesta.Succeeded ? true : false;

        }

        public async Task<bool> EliminarRoleIdAsync(Roles role)
        {

            var respuesta = await _roleManager.DeleteAsync(role);

            return respuesta.Succeeded ? true : false;

        }

    }
}
