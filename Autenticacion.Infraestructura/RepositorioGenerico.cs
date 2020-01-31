
using Autenticacion.Dominio.Repositorio.Contratos;
using System;
using System.Collections.Generic;

namespace Autenticacion.Infraestructura
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {

        private readonly AutenticationDbContext _autenticationDbContext;

        public RepositorioGenerico(AutenticationDbContext autenticationDbContext)
        {

            _autenticationDbContext = autenticationDbContext;

        }

        public virtual void Actualizar(T entity)
        {
            _autenticationDbContext.Set<T>().Update(entity);
        }

        public virtual void Crear(T entity)
        {
            _autenticationDbContext.Set<T>().Add(entity);
        }

        public virtual void Eliminar(T entity)
        {
            _autenticationDbContext.Set<T>().Remove(entity);
        }

        public virtual IEnumerable<T> Obtener()
        {
            return _autenticationDbContext.Set<T>();
        }

        public virtual T ObtenerPorId(Guid id)
        {
            return _autenticationDbContext.Set<T>().Find(id);
        }
    }
}
