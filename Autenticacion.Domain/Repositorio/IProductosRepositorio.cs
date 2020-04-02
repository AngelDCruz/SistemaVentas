using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Dominio.Repositorio
{
    public interface  IProductosRepositorio
    {

        IQueryable<ProductosEntidad> ObtenerProductosAsync();

        Task<ProductosEntidad> ObtenerProductoPorIdAsync(Guid id);

        Task<ProductosEntidad> ObtenerProductoPorCodigoAsync(string codigo);

        Task<ProductosEntidad> CrearProductoAsync(ProductosEntidad producto);

        Task<bool> ActualizarProductoAsync(ProductosEntidad producto);

        Task<bool> EliminarProductoAsync(ProductosEntidad producto);

    }
}
