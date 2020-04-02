using Autenticacion.Infraestructura;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Infraestructura.Repositorio
{
    public class PersonaRepositorio : IPersonaRepositorio
    {

        private readonly AppDbContext _context;

        public PersonaRepositorio(AppDbContext context)
        {

            _context = context;

        }

        public IQueryable<PersonaEntidad> ObtenerPersonas(string tipoPersona)
        {

            return  _context.Personas.OrderBy(x => x.Estatus).Where(x => x.TipoPersona == tipoPersona).AsQueryable();

        }

        public async Task<PersonaEntidad> ObtenerPersonaIdAsync(Guid Id, string tipoPersona)
        {

            return await  _context.Personas.FirstOrDefaultAsync(x => x.Id == Id &&  x.TipoPersona == tipoPersona);

        }

        public async Task<PersonaEntidad> CrearPersonaAsync(PersonaEntidad persona)
        {

            await _context.Personas.AddAsync(persona);

            return await _context.SaveChangesAsync() > 0 ? persona : null;

        }
        public async Task<bool> ActualizarPersonaAsync(PersonaEntidad persona)
        {

           _context.Personas.Update(persona);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> EliminarPersonaAsync(PersonaEntidad persona)
        {

            _context.Personas.Remove(persona);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<PersonaEntidad> ObtenerPersonaNumDocumento(string numDocumento, string tipoPersona)
        {

            return await _context.Personas
                .FirstOrDefaultAsync(x => x.NumDocumento == numDocumento && x.TipoPersona == tipoPersona);

        }
    }
}
