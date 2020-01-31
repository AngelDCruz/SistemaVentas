using Autenticacion.Dominio.Repositorio.Contratos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Infraestructura
{
    public class UnitOfWork : IUnitOfWork
    {

        private AutenticationDbContext _autenticationDbContext;
        private IRepositorioGenerico<IdentityUser> _usuarioRepositorio;

        public UnitOfWork(AutenticationDbContext autenticationDbContext)
        {

            _autenticationDbContext = autenticationDbContext;

        }

        public IRepositorioGenerico<IdentityUser> UsuarioRepositorio 
        {
            get {
                return _usuarioRepositorio = _usuarioRepositorio ?? new RepositorioGenerico<IdentityUser>(_autenticationDbContext);
            }
        }

        public void Guardar()
        {
            throw new NotImplementedException();
        }

    }
}
