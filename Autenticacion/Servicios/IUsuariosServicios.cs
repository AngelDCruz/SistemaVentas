using Autenticacion.Api.DTO.Respuestas.v1;
using Autenticacion.Api.DTO.Solicitudes.v1;
using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Api.Servicios
{
    public interface IUsuariosServicios
    {

        Task<List<Usuarios>> ObtenerUsuariosAsync();

        Task<Usuarios> ObtenerUsuarioIdAsync(Guid id);

        Task<Usuarios> ObtenerUsuarioEmailAsync(string email);

        Task<Guid> CrearUsuarioAsync(Usuarios usuario);

        Task<bool> CrearUsuarioRoleAsync(List<UsuariosRoles> lstUsuariosRoles);

        Task<bool> ActualizarUsuarioAsync(Usuarios usuario);

        Task<bool> EliminarUsuarioAsync(Usuarios usuario);

    }
}
