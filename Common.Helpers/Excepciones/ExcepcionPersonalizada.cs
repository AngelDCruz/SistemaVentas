using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Excepciones
{
    public abstract class ExcepcionPersonalizada : Exception
    {

        private int _codigoHttp;
        private string _descripcion;

        public string Descripcion { get => _descripcion; }
        public int CodigoHttp { get => _codigoHttp; }

        public ExcepcionPersonalizada(string descripcion =  "", int codigoHttp = 400) : base(descripcion)
        {

            _descripcion = descripcion;
            _codigoHttp = codigoHttp;

        }

    }
}
