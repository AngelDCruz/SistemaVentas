using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Paginacion;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<CategoriasEntidad>> ObtenerCategoriasAsync(FiltroPagina filtro)
        {

             var lstCategorias = _categoriaRepositorio.ObtenerCategoriasAsync(); 

            if ( filtro != null )
            {

                var pagina = (filtro.Pagina  -1) * filtro.Limite;
                var limite = filtro.Limite;

                lstCategorias = lstCategorias.Skip(pagina).Take(limite);

            }

            return await lstCategorias.ToListAsync(); 

        }


        public async Task<CategoriasEntidad> ObtenerCategoriaPorIdAsync(Guid id)
        {

            return await _categoriaRepositorio.ObtenerCategoriaPorIdAsync(id);

        }

        public async Task<CategoriasEntidad> ObtenerCategoriaPorNombreAsync(string nombre)
        {

            return await _categoriaRepositorio.ObtenerCategoriaPorNombreAsync(nombre);

        }


        public async Task<CategoriasEntidad> CrearCategoriaAsync(CategoriasEntidad categoria)
        {

            categoria.Estatus = "Act";

            return await _categoriaRepositorio.CrearCategoriaAsync(categoria);

        }

        public async Task<bool> ActualizarCategoriaAsync(CategoriasEntidad categoria)
        {

            var categoriaActualizar = await _categoriaRepositorio.ObtenerCategoriaPorIdAsync(categoria.Id);
            categoriaActualizar.Nombre = categoria.Nombre;
            categoriaActualizar.Descripcion = categoria.Descripcion;

            return await _categoriaRepositorio.ActualizarCategoriaAsync(categoriaActualizar);

        }


        public async Task<bool> EliminarCategoriaAsync(CategoriasEntidad categoria)
        {

            return await _categoriaRepositorio.EliminarCategoriaAsync(categoria);

        }

        public async Task<List<CategoriasEntidad>> ObtenerFiltroCategoriasAsync(string nombre)
        {

            var lstCategorias = _categoriaRepositorio.ObtenerCategoriasAsync();

            if(lstCategorias == null)
            {
                return null;
            }

            return await lstCategorias.Where(x => x.Nombre.Contains(nombre)).ToListAsync();

        }
    }
}
