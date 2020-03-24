using SistemaVentas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Dominio.Repositorio
{
    public interface ICategoriaRepositorio
    {

        IQueryable<CategoriasEntidad> ObtenerCategoriasAsync();

        Task<CategoriasEntidad> ObtenerCategoriaPorIdAsync(Guid id);

        Task<CategoriasEntidad> ObtenerCategoriaPorNombreAsync(string nombre);

        Task<CategoriasEntidad> CrearCategoriaAsync(CategoriasEntidad categoria);

        Task<bool> ActualizarCategoriaAsync(CategoriasEntidad categoria);

        Task<bool> EliminarCategoriaAsync(CategoriasEntidad categoria);

    }
}
