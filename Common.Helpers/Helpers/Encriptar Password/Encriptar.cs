using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SistemaVentas.Infraestructura.Transversal.Helpers.Encriptar_Password
{
    public class Encriptar
    {
        public static string GeneraClaveHash(string pass, string saltKey)
        {
            HashAlgorithm algoritoHash = HashAlgorithm.Create("SHA1");
            byte[] hash = algoritoHash.ComputeHash(Encoding.UTF8.GetBytes(pass + saltKey));

            return BitConverter.ToString(hash).Replace("-", "");
        }

        public static string GenerarSaltKey(int longitud)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            Byte[] buff = new byte[longitud - 1];

            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);

        }
    }
}
