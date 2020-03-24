using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Infraestructura.Transversal.Helpers.Base64
{
    public class ConvertirBase64
    {

        public static string Convertir64String(string cadena)
        {

            var convertirBase64 = System.IO.File.ReadAllBytes(cadena);

            return Convert.ToBase64String(convertirBase64);

        }

    }
}
