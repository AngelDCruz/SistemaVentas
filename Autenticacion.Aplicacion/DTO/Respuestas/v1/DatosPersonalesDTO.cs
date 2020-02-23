using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Aplicacion.DTO.Respuestas.v1
{
    public class DatosPersonalesDTO
    {

        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }
        
        public string ApellidoMaterno { get; set; }

        public string Pais { get; set; }

        public string Ciudad { get; set; }

        public string Calle { get; set; }

        public string Telefono { get; set; }

        public Guid UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string Estatus { get; set; }


    }
}
