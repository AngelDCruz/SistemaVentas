using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.Repositorio.Contratos
{
    public interface IRepositorioGenerico <T> where T : class
    {

        T ObtenerPorId(Guid id);

        IEnumerable<T> Obtener();

        void Crear(T entity);

        void Actualizar(T entity);

        void Eliminar(T entity);

    }
}
