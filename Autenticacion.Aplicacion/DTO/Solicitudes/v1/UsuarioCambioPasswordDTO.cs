using System;
using System.Collections.Generic;
using System.Text;

namespace Autenticacion.Aplicacion.DTO.Solicitudes.v1
{
    public class UsuarioCambioPasswordDTO
    {

        public string PasswordAnterior { get; set; }

        public string PasswordNueva { get; set; }

    }
}
