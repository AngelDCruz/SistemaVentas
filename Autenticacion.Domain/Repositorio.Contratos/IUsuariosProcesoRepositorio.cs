using System;
using System.Threading.Tasks;
using Autenticacion.Dominio.Entidades;

namespace Autenticacion.Dominio.Repositorio.Contratos
{
    public interface IUsuariosProcesoRepositorio
    {

        Task<Usuarios> CrearUsuario(Usuarios usuario);

        Task<bool> ActualizarUsuario(Usuarios usuario);

        Task<bool> EliminarUsuario(Usuarios usuario);

    }
}
