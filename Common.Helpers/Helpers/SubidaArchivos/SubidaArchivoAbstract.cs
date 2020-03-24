using Microsoft.AspNetCore.Http;
using SistemaVentas.Infraestructura.Transversal.Helpers.GenerarCadena;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace SistemaVentas.Infraestructura.Transversal.Helpers.SubidaArchivos
{
    public abstract class SubidaArchivoAbstract
    {

        protected abstract bool ValidarFormato(IFormFile archivo);

        protected abstract bool ValidarPeso(IFormFile archivo, decimal tamanoPermitido = 200000);

        public abstract ArchivoModelo CargarArchivo(IFormFile archivo, string directorio);

        protected ArchivoModelo SubirArchivo(IFormFile archivo, string ruta)
        {

            if(validarNullOVacio(archivo, ruta))
            {


                // SI NO EXISTE DIRECTORIO CREALO
                if (!Directory.Exists(ruta))
                {

                    Directory.CreateDirectory(ruta);

                }

                if (Directory.Exists(ruta))
                {

                    var nombreAleatorio = GenerarCadenas.Generar();

                    var nuevoNombreArchivo = $"{nombreAleatorio}{Environment.TickCount}{Path.GetExtension(archivo.FileName)}";

                    using (var formFileStream = new FileStream(Path.Combine(ruta, nuevoNombreArchivo), FileMode.Create, FileAccess.ReadWrite))
                    {

                        archivo.CopyTo(formFileStream);

                        ruta += $"\\{nuevoNombreArchivo}";

                    };

                }
             
            }

            return new ArchivoModelo
            {
                Url = ruta,
                Formato = Path.GetExtension(archivo.FileName),
                Codex = GenerarCodex(archivo.ContentType)
            };

        }

        private string GenerarCodex(string contentType)
        {

            return $"data:{contentType}; base64, ";

        }

        public bool validarNullOVacio(IFormFile archivo, string rutaDestino)
        {

            if (archivo == null || string.IsNullOrEmpty(rutaDestino)) return false;

            return true;

        }

    }

    public class ArchivoModelo
    {

        public string Url { get; set; }

        public string Formato { get; set; }

        public string Codex { get; set; }

    }

}
