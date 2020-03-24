using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Common.Paginacion
{

    public class Respuesta<T>
    {

        public Respuesta(T Entidad)
        {
            Datos = Entidad;
        }

        public T Datos { get; set; }

    }


    public class FiltroPagina
    {

        [FromQuery]
        public bool Paginar { get; set; } = false;

        [FromQuery]
        public int Limite { get; set; } = 1000;

        [FromQuery]
        public int Pagina { get; set; } = 1;

    }

}
