using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Repositorio.Contratos
{
    public interface IRolesProcesoRepositorio
    {

        Task<Roles> CrearRoleAsync(Roles role);

        Task<bool> ActualizarRoleAsync(Roles role);

        Task<bool> EliminarRoleIdAsync(Roles Role);

    }
}
