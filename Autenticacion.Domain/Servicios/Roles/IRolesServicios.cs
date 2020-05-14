

using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Autenticacion.Dominio.Servicios.Roles
{
    public interface IRolesServicios
    {

        Task<List<RolesEntidad>> ObtenerRolesAsync();

        Task<RolesEntidad> ObtenerRoleIdAsync(Guid id);

        Task<RolesEntidad> ObtenerRoleNombreAsync(string nombre);

        Task<RolesEntidad> CrearRoleAsync(RolesEntidad role);

        Task<bool> ActualizarRoleAsync(RolesEntidad role);

        Task<bool> EliminarRoleAsync(RolesEntidad role);

        Task<bool> ActivarRolePorIdAsync(Guid id);

        Task<List<RolesEntidad>> BusquedaRoleAsync(string nombre);

    }
}
