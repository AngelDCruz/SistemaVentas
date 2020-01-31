using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Dominio.Repositorio.Contratos
{
    public interface IUnitOfWork
    {

        IRepositorioGenerico<IdentityUser> UsuarioRepositorio { get; }

        void Guardar();

    }
}
