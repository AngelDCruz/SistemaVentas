using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Repositorio.Contratos
{
    public interface IUsuariosInformacionRepositorio
    {

        IQueryable<Usuarios> ObtenerUsuarios();

        Task<Usuarios> ObtenerUsuarioPorIdAsync(Guid id);

        Task<Usuarios> ObtenerUsuarioEmailAsync(string email);

    }
}
