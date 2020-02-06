using Microsoft.AspNetCore.Mvc;

namespace Autenticacion.Dominio.DTO.Solicitudes.v1
{
    public class IncluirUsuariosDTO
    {

        [FromQuery]
        public bool Role { get; set; } = false;

    }
}
