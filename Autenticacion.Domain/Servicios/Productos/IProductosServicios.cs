using Common.Paginacion;
using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Dominio.Servicios.Productos
{
    public interface IProductosServicios
    {

        Task<List<ProductosEntidad>> ObtenerProductosAsync(FiltroPagina filtro, string Estatus = "todos");

        Task<ProductosEntidad> ObtenerProductoPorIdAsync(Guid id);

        Task<ProductosEntidad> ObtenerProductoPorCodigoAsync(string codigo);

        Task<ProductosEntidad> CrearProductoAsync(ProductosEntidad producto);

        Task<bool> ActualizarProductoAsync(ProductosEntidad producto);

        Task<bool> ActualizarImagenProductoAsync(Guid idProducto, string imagen); 

        Task<bool> EliminarProductoAsync(ProductosEntidad producto);

        Task<List<ProductosEntidad>> ObtenerProductosFiltroAsync(string nombre, string codigo);

        Task<bool> ActivarProductoAsync(Guid idProducto);

    }
}
