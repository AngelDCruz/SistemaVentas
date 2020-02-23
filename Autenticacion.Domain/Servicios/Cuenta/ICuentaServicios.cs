using Autenticacion.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.Dominio.Servicios.Cuenta
{
    public interface ICuentaServicios
    {

        Task<bool> RestaurarPasswordCuentaAsync(string passwordAnterior, string passwordNueva);

        Task<string> CambiarImagenPerfilAsync(Guid idUsuario, string imagenURL);

        Task<bool> CambiarNombrePerfilAsync(Guid idUsuario, string nombreUsuario);

    }
}
