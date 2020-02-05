using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Repositorio.Contratos
{
    public interface IRolesInfoRepositorio
    {

        Task<List<Roles>> ObtenerRolesAsync();

        Task<Roles> ObtenerRoleIdAsync(Guid id);

        Task<Roles> ObtenerRoleNombreAsync(string nombre);

    }
}
