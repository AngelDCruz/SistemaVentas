using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Common.Paginacion
{

    public class Paginacion
    {

        public int Actual { get; set;  }

        public int Anterior { get; set; }

        public int Siguiente { get; set; }

        public int Total { get; set; }

    }

    public class Paginador<T> 
    {

        public Paginador(T datos, FiltroPagina filtro)
        {
            Datos = datos;
        }

        public T Datos { get;}

        public Paginacion Paginacion { get;}

    }



    public class FiltroPagina
    {

        [FromQuery]
        public int Limite { get; set; } = 1;

        [FromQuery]
        public int Pagina { get; set; } = 1;

    }

}
