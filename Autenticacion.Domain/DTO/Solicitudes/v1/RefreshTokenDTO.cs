using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autenticacion.Dominio.DTO.Solicitudes.v1
{
    public class RefreshTokenDTO
    {

        [Required(ErrorMessage = "El token es requerido")]
        public string Access_Token { get; set; }

        [Required(ErrorMessage = "El refresh token es requerido")]
        public string Refresh_Token { get; set; }

    }
}
