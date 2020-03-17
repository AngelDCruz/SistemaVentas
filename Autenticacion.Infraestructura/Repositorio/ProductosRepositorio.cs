
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Autenticacion.Infraestructura;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Repositorio;

namespace SistemaVentas.Infraestructura.Repositorio
{
    public class ProductosRepositorio : IProductosRepositorio
    {

        private readonly AppDbContext _context;

        public ProductosRepositorio(AppDbContext context)
        {

            _context = context;

        }

        public async Task<ProductosEntidad> ObtenerProductoPorIdAsync(Guid id)
        {

            return await _context.Productos.FirstOrDefaultAsync(x => x.Id == id && x.Estatus != "Baj");

        }

        public async Task<List<ProductosEntidad>> ObtenerProductosAsync()
        {

            return await _context.Productos.Where(x => x.Estatus != "Baj").ToListAsync();

        }

        public async Task<ProductosEntidad> CrearProductoAsync(ProductosEntidad producto)
        {

            await _context.Productos.AddAsync(producto);

            return await _context.SaveChangesAsync() > 0 ? producto : null;

        }

        public async Task<bool> ActualizarProductoAsync(ProductosEntidad producto)
        {

            _context.Productos.Update(producto);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> EliminarProductoAsync(ProductosEntidad producto)
        {

            _context.Productos.Remove(producto);

            return await _context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<ProductosEntidad> ObtenerProductoPorCodigoAsync(string codigo)
        {

            return await _context.Productos.FirstOrDefaultAsync(x => x.Codigo == codigo);

        }
    }
}
