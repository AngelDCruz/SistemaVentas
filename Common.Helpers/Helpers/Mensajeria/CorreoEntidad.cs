using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Mensajeria
{
    public class CorreoEntidad
    {

        public string Servidor { get; set; }

        public int Puerto { get; set; }

        public bool? Ssl { get; set; }

        public string De { get; set;}
        
        public string Para { get; set; }

        public string Password { get; set; }

        public string Mensaje { get; set; }

    }
}
