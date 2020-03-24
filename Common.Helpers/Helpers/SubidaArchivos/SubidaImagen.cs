using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Common.Excepciones;
using Microsoft.AspNetCore.Http;

namespace SistemaVentas.Infraestructura.Transversal.Helpers.SubidaArchivos
{
    public class SubidaImagen :  SubidaArchivoAbstract
    {

        public override ArchivoModelo CargarArchivo(IFormFile archivo, string directorio)
        {

            if (ValidarPeso(archivo))
            {

                throw new Excepcion("La imagen excede el limite de peso, tamaño máximo 1mb");

            }

            if (!ValidarFormato(archivo))
            {

                throw new Excepcion("El formato de la imagen no es el requerido. (.jpg, .png, .jpeg)");

            }

            return SubirArchivo(archivo, directorio);

        }

        protected override bool ValidarFormato(IFormFile archivo)
        {
            Dictionary<string, string> lstFormatosAceptados = new Dictionary<string, string>();
            lstFormatosAceptados.Add("jpg", ".jpg");
            lstFormatosAceptados.Add("jpeg", ".jpeg");
            lstFormatosAceptados.Add("png", ".png");

            var extencionActual = Path.GetExtension(archivo.FileName);

            return lstFormatosAceptados.ContainsValue(extencionActual);
        }

        protected override bool ValidarPeso(IFormFile archivo, decimal tamanoPermitido = 200000)
        {
            return (archivo.Length > tamanoPermitido) ? true : false;

        }

    }
}
