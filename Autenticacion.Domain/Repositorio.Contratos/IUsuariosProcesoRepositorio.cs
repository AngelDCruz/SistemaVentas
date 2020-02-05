using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autenticacion.Dominio.Entidades;

namespace Autenticacion.Dominio.Repositorio.Contratos
{
    public interface IUsuariosProcesoRepositorio
    {

        Task<Guid> CrearUsuarioAsync(Usuarios usuario);

        Task<bool> CrearUsuarioRoleAsync(List<UsuariosRoles> lstUsuariosRoles);

        Task<bool> ActualizarUsuarioAsync(Usuarios usuario);

        Task<bool> EliminarUsuarioAsync(Usuarios usuarios);

    }
}
