


using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SistemaVentas.Dominio.Entidades;

namespace SistemaVentas.Dominio.Servicios.Roles
{
    public interface IRolesServicios
    {

        Task<List<RolesEntidad>> ObtenerRolesAsync();

        Task<RolesEntidad> ObtenerRoleIdAsync(Guid id);

        Task<RolesEntidad> ObtenerRoleNombreAsync(string nombre);

        Task<RolesEntidad> CrearRoleAsync(RolesEntidad role);

        Task<bool> ActualizarRoleAsync(RolesEntidad role);

        Task<bool> EliminarRoleAsync(RolesEntidad role);

    }
}
