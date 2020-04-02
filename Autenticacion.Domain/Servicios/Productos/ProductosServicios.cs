using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Paginacion;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<ProductosEntidad>> ObtenerProductosAsync(FiltroPagina filtro, string Estatus = "todos")
        {

            var lstProductos = _productosRepositorio.ObtenerProductosAsync();

            if (filtro != null)
            {

                var pagina = (filtro.Pagina  -1 ) * filtro.Limite;
                var limite = filtro.Limite;

                lstProductos.Skip(pagina).Take(limite);

            }

           if (Estatus == "bajas") return await lstProductos.Where(x => x.Estatus == "Baj").ToListAsync();

            if (Estatus == "activos") return await lstProductos.Where(x => x.Estatus == "Act").ToListAsync();

            return await lstProductos.ToListAsync();

        }

        public async Task<ProductosEntidad> ObtenerProductoPorIdAsync(Guid id)
        {

            return await _productosRepositorio.ObtenerProductoPorIdAsync(id);

        }

        public async Task<ProductosEntidad> CrearProductoAsync(ProductosEntidad producto)
        {

            return await _productosRepositorio.CrearProductoAsync(producto);

        }

        public async Task<bool> ActualizarProductoAsync(ProductosEntidad producto)
        {

            var productoActualizar = await _productosRepositorio.ObtenerProductoPorIdAsync(producto.Id);
            productoActualizar.Nombre = producto.Nombre;
            productoActualizar.Descripcion = producto.Descripcion;
            productoActualizar.Codigo = producto.Codigo;
            productoActualizar.CategoriaId = producto.CategoriaId;

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

        public Task<List<ProductosEntidad>> ObtenerProductosFiltroAsync(string nombre, string codigo)
        {

            var lstProductos = _productosRepositorio.ObtenerProductosAsync();

            if(string.Empty != nombre)
            {

                lstProductos = lstProductos.Where(x => x.Nombre.Contains(nombre));

            }

            if(string.Empty != codigo)
            {

                lstProductos = lstProductos.Where(x => x.Codigo.Contains(codigo));

            }

            return lstProductos.ToListAsync();

        }

        public async Task<bool> ActualizarImagenProductoAsync(Guid idProducto, string imagen)
        {

            var actualizarProducto = await _productosRepositorio.ObtenerProductoPorIdAsync(idProducto);
            actualizarProducto.Imagen = imagen;

            return await _productosRepositorio.ActualizarProductoAsync(actualizarProducto);

        }

        public async Task<bool> ActivarProductoAsync(Guid idProducto)
        {

            var actualizarProducto = await _productosRepositorio.ObtenerProductoPorIdAsync(idProducto);
            actualizarProducto.Estatus = "Act";

            return await _productosRepositorio.ActualizarProductoAsync(actualizarProducto);

        }
    }
}
