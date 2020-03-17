using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Repositorio;

namespace SistemaVentas.Dominio.Servicios.Categoria
{
    public class CategoriaServicios : ICategoriaServicios
    {

        public ICategoriaRepositorio _categoriaRepositorio;

        public CategoriaServicios(ICategoriaRepositorio categoriaRepositorio)
        {

            _categoriaRepositorio = categoriaRepositorio;

        }

        public async Task<bool> ActualizarCategoriaAsync(CategoriasEntidad categoria)
        {

            var categoriaActualizar = await _categoriaRepositorio.ObtenerCategoriaPorIdAsync(categoria.Id);
            categoriaActualizar.Nombre = categoria.Nombre;
            categoriaActualizar.Descripcion = categoria.Descripcion;

            return await _categoriaRepositorio.ActualizarCategoriaAsync(categoriaActualizar);

        }

        public async Task<CategoriasEntidad> CrearCategoriaAsync(CategoriasEntidad categoria)
        {

            return await _categoriaRepositorio.CrearCategoriaAsync(categoria);

        }

        public async Task<bool> EliminarCategoriaAsync(CategoriasEntidad categoria)
        {

            return await _categoriaRepositorio.EliminarCategoriaAsync(categoria);

        }

        public async Task<CategoriasEntidad> ObtenerCategoriaPorIdAsync(Guid id)
        {

            return await _categoriaRepositorio.ObtenerCategoriaPorIdAsync(id);

        }

        public async Task<CategoriasEntidad> ObtenerCategoriaPorNombreAsync(string nombre)
        {

            return await _categoriaRepositorio.ObtenerCategoriaPorNombreAsync(nombre);

        }

        public async Task<List<CategoriasEntidad>> ObtenerCategoriasAsync()
        {

            return await _categoriaRepositorio.ObtenerCategoriasAsync();

        }
    }
}
