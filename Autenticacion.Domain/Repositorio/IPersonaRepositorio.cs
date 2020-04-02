
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaVentas.Dominio.Entidades;

namespace SistemaVentas.Dominio.Repositorio
{
    public interface IPersonaRepositorio
    {

        IQueryable<PersonaEntidad> ObtenerPersonas(string tipoPersona);

        Task<PersonaEntidad> ObtenerPersonaIdAsync(Guid Id, string tipoPersona);

        Task<PersonaEntidad> ObtenerPersonaNumDocumento(string numDocumento, string tipoPersona);

        Task<PersonaEntidad> CrearPersonaAsync(PersonaEntidad persona);

        Task<bool> ActualizarPersonaAsync(PersonaEntidad persona);

        Task<bool> EliminarPersonaAsync(PersonaEntidad persona);

    }
}
