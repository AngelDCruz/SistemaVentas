using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Dominio.Repositorio;

namespace SistemaVentas.Dominio.Servicios.Productos
{
    public class ProductosServicios : IProductosServicios
    {

        private readonly IProductosRepositorio _productosRepositorio;

        public ProductosServicios(IProductosRepositorio productosRepositorio)
        {

            _productosRepositorio = productosRepositorio;

        }

        public async Task<ProductosEntidad> ObtenerProductoPorIdAsync(Guid id)
        {

            return await _productosRepositorio.ObtenerProductoPorIdAsync(id);

        }

        public async Task<List<ProductosEntidad>> ObtenerProductosAsync()
        {

            return await _productosRepositorio.ObtenerProductosAsync();

        }

        public async Task<ProductosEntidad> CrearProductoAsync(ProductosEntidad producto)
        {

            return await _productosRepositorio.CrearProductoAsync(producto);

        }

        public async Task<bool> ActualizarProductoAsync(ProductosEntidad producto)
        {

            var productoActualizar = await _productosRepositorio.ObtenerProductoPorIdAsync(producto.Id);
            productoActualizar.Nombre = producto.Nombre;
            producto.Descripcion = producto.Descripcion;
            producto.Imagen = producto.Imagen;

            return await _productosRepositorio.ActualizarProductoAsync(productoActualizar);

        }


        public async Task<bool> EliminarProductoAsync(ProductosEntidad producto)
        {

            return await _productosRepositorio.EliminarProductoAsync(producto);

        }

        public async Task<ProductosEntidad> ObtenerProductoPorCodigoAsync(string codigo)
        {

            return await _productosRepositorio.ObtenerProductoPorCodigoAsync(codigo);

        }
    }
}
