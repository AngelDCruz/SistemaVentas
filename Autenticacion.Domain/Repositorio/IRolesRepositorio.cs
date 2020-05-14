using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Repositorio
{
    public interface IRolesRepositorio
    {

        Task<List<RolesEntidad>> ObtenerRolesAsync();

        Task<RolesEntidad> ObtenerRoleIdAsync(Guid id);

        Task<RolesEntidad> ObtenerRoleNombreAsync(string nombre);

        Task<IEnumerable<Claim>> ObtenerRoleClaimAsync(RolesEntidad roles);

        Task<RolesEntidad> CrearRoleAsync(RolesEntidad role);

        Task<bool> ActualizarRoleAsync(RolesEntidad role);

        Task<bool> EliminarRoleIdAsync(RolesEntidad Role);

        Task<List<RolesEntidad>> ObtenerRolesNombreBusquedaAsync(string nombre);

    }
}
