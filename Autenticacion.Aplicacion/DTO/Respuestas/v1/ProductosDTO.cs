using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Aplicacion.DTO.Respuestas.v1
{
    public class ProductosDTO
    {

        public Guid Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Codigo { get; set; }

        public string Imagen { get; set; }

        public Guid Categoria { get; set; }

        public string Estatus { get; set; }

    }
}
