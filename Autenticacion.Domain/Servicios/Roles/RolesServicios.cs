


using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio;

namespace Autenticacion.Dominio.Servicios.Roles
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

        public async Task<List<RolesEntidad>> ObtenerRolesAsync()
        {

            return await _rolesInfoRepositorio.ObtenerRolesAsync();

        }

        public async Task<RolesEntidad> ObtenerRoleIdAsync(Guid id)
        {

            return await _rolesInfoRepositorio.ObtenerRoleIdAsync(id);

        }

        public async Task<RolesEntidad> ObtenerRoleNombreAsync(string nombre)
        {

            return await _rolesInfoRepositorio.ObtenerRoleNombreAsync(nombre);

        }

        public async Task<RolesEntidad> CrearRoleAsync(RolesEntidad role)
        {

            role.Estatus = "Act";
            return await _rolesInfoRepositorio.CrearRoleAsync(role);

        }

        public async Task<bool> ActualizarRoleAsync(RolesEntidad role)
        {

            return await _rolesInfoRepositorio.ActualizarRoleAsync(role);

        }

        public async Task<bool> EliminarRoleAsync(RolesEntidad role)
        {

            return await _rolesInfoRepositorio.EliminarRoleIdAsync(role);

        }

    }
}
