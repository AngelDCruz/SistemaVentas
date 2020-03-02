
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using SistemaVentas.Dominio.Entidades;

namespace SistemaVentas.Dominio.Repositorio.Contratos
{
    public interface IRolesRepositorio
    {

        Task<List<RolesEntidad>> ObtenerRolesAsync();

        Task<RolesEntidad> ObtenerRoleIdAsync(Guid id);

        Task<RolesEntidad> ObtenerRoleNombreAsync(string nombre);

        Task<RolesEntidad> CrearRoleAsync(RolesEntidad role);

        Task<bool> ActualizarRoleAsync(RolesEntidad role);

        Task<bool> EliminarRoleIdAsync(RolesEntidad Role);

    }
}
