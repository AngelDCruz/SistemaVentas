

using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio.Contratos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autenticacion.Api.Servicios
{
    public class RolesServicios : IRolesServicios
    {


        private readonly IRolesRepositorio _rolesInfoRepositorio;

        public RolesServicios(
            IRolesRepositorio rolesInfoRepositorio
        )
        {

            _rolesInfoRepositorio = rolesInfoRepositorio;
        
        }

        public async Task<List<Roles>> ObtenerRolesAsync()
        {

            return await _rolesInfoRepositorio.ObtenerRolesAsync();

        }

        public async Task<Roles> ObtenerRoleIdAsync(Guid id)
        {

            return await _rolesInfoRepositorio.ObtenerRoleIdAsync(id);

        }

        public async Task<Roles> ObtenerRoleNombreAsync(string nombre)
        {

            return await _rolesInfoRepositorio.ObtenerRoleNombreAsync(nombre);

        }

        public async Task<Roles> CrearRoleAsync(Roles role)
        {

            return await _rolesInfoRepositorio.CrearRoleAsync(role);

        }

        public async Task<bool> ActualizarRoleAsync(Roles role)
        {

            return await _rolesInfoRepositorio.ActualizarRoleAsync(role);

        }

        public async Task<bool> EliminarRoleAsync(Roles role)
        {

            return await _rolesInfoRepositorio.EliminarRoleIdAsync(role);

        }

    }
}
