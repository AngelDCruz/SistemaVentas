﻿

using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio.Contratos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autenticacion.Api.Servicios
{
    public class RolesServicios : IRolesServicios
    {


        private readonly IRolesInfoRepositorio _rolesInfoRepositorio;
        private readonly IRolesProcesoRepositorio _rolesProcesoRepositorio;

        public RolesServicios(
            IRolesInfoRepositorio rolesInfoRepositorio,
            IRolesProcesoRepositorio rolesProcesoRepositorio
        )
        {

            _rolesInfoRepositorio = rolesInfoRepositorio;
            _rolesProcesoRepositorio = rolesProcesoRepositorio;



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

            return await _rolesProcesoRepositorio.CrearRoleAsync(role);

        }

        public async Task<bool> ActualizarRoleAsync(Roles role)
        {

            return await _rolesProcesoRepositorio.ActualizarRoleAsync(role);

        }

        public async Task<bool> EliminarRoleAsync(Roles role)
        {

            return await _rolesProcesoRepositorio.EliminarRoleIdAsync(role);

        }

    }
}
