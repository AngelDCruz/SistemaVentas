using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Aplicacion.DTO.Solicitudes.v1
{
    public class FiltroPersonaDTO
    {

        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string TipoDocumento { get; set; }

        public string NumDocumento { get; set; }

    }
}
