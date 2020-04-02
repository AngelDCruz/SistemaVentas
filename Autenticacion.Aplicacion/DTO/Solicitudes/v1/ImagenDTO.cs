using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Aplicacion.DTO.Solicitudes.v1
{
    public class ImagenDTO
    {
        public IFormFile Imagen { get; set; }
    }
}
