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

        public  IQueryable<CategoriasEntidad> ObtenerCategoriasAsync()
        {

            return   _context.Categorias.OrderBy(x => x.Estatus).AsQueryable();

        }

        public async Task<CategoriasEntidad> ObtenerCategoriaPorIdAsync(Guid id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CategoriasEntidad> CrearCategoriaAsync(CategoriasEntidad categoria)
        {

            categoria.Estatus = "Act";

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
