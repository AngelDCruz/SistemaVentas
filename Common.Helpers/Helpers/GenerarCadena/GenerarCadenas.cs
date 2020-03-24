using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVentas.Infraestructura.Transversal.Helpers.GenerarCadena
{
    public class GenerarCadenas
    {

        public static string Generar(int longitudPredeterminado = 10)
        {

            int longitudMax = longitudPredeterminado;
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int longitud = caracteres.Length;
            char letra;
            Random rd = new Random();

            string cadenaGenerada = string.Empty;

            for(int i = 0; i < longitudMax;  i++)
            {

                letra = caracteres[rd.Next(longitud)];

                cadenaGenerada += letra.ToString();

            }

            return cadenaGenerada;

        }

    }
}
