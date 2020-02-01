using Autenticacion.Api.DTO.Respuestas.v1;
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

        Task<Usuarios> ObtenerUsuarioAsync(Guid id);

        Task<Usuarios> CrearUsuarioAsync(Usuarios solicitud);

        Task<bool> ActualizarUsuarioAsync(Usuarios solicitud);

        Task<bool> EliminarUsuarioAsync(Usuarios id);

    }
}
