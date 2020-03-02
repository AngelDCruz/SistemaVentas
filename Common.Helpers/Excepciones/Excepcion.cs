using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Excepciones
{
   public class Excepcion : ExcepcionPersonalizada
    {
        public Excepcion(string descripcion = "", int codigoHttp = 400)  : base(descripcion, codigoHttp)
        {
        }
    }
}
