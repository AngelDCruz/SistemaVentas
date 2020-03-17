using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autenticacion.Infraestructura;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Repositorio;

namespace SistemaVentas.Infraestructura.Repositorio
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {

        private readonly AppDbContext _context;

        public CategoriaRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriasEntidad>> ObtenerCategoriasAsync()
        {

            return  await _context.Categorias.Where(x => x.Estatus != "Baj").ToListAsync();

        }

        public async Task<CategoriasEntidad> ObtenerCategoriaPorIdAsync(Guid id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(x => x.Id == id && x.Estatus != "Baj");
        }

        public async Task<CategoriasEntidad> CrearCategoriaAsync(CategoriasEntidad categoria)
        {

            await _context.Categorias.AddAsync(categoria);

            return await _context.SaveChangesAsync() > 0 ? categoria : null;

        }

        public async Task<bool> ActualizarCategoriaAsync(CategoriasEntidad categoria)
        {

            _context.Categorias.Update(categoria);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> EliminarCategoriaAsync(CategoriasEntidad categoria)
        {

            _context.Categorias.Remove(categoria);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<CategoriasEntidad> ObtenerCategoriaPorNombreAsync(string nombre)
        {

            return await _context.Categorias.FirstOrDefaultAsync(x => x.Nombre == nombre);

        }
    }
}
