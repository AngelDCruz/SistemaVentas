using System;
using System.Threading.Tasks;
using Autenticacion.Dominio.Entidades;

namespace Autenticacion.Dominio.Repositorio.Contratos
{
    public interface IUsuariosProcesoRepositorio
    {

        Task<Guid> CrearUsuario(Usuarios usuario);

        Task<bool> ActualizarUsuario(Usuarios usuario);

        Task<bool> EliminarUsuario(Guid id);

    }
}
