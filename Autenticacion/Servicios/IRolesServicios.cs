using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Api.Servicios
{
    public interface IRolesServicios
    {

        Task<List<Roles>> ObtenerRolesAsync();

        Task<Roles> ObtenerRoleIdAsync(Guid id);

        Task<Roles> ObtenerRoleNombreAsync(string nombre);

        Task<Roles> CrearRoleAsync(Roles role);

        Task<bool> ActualizarRoleAsync(Roles role);

        Task<bool> EliminarRoleAsync(Roles role);

    }
}
