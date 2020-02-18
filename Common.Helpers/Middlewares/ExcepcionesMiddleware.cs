using Common.Excepciones;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Middlewares
{

    public class ExcepcionesMiddleware
    {

        //DELEGADO
        private RequestDelegate _next { get; set; }
        private readonly Serilog.ILogger _logger;


        public ExcepcionesMiddleware(RequestDelegate next, Serilog.ILogger logger)
        {

            _next = next;
            _logger = logger;

        }


        public async Task Invoke(HttpContext context)
        {


            try
            {

                await _next.Invoke(context);

            }
            catch (Exception ex)
            {

                await HandleBadRequestExceptionAsync(context, ex);
                await HandleInternalServerErrorExceptionAsync(context, ex);

            }


        }

        private async Task HandleBadRequestExceptionAsync(HttpContext context, Exception ex)
        {

            var respuesta = context.Response;
            var excepcionBase = ex as ExcepcionPersonalizada;
            var estatusCodigo = (int)HttpStatusCode.BadRequest;
            var mensaje = "";
            var descripcion = "";


            if (excepcionBase != null)
            {

                estatusCodigo = excepcionBase.CodigoHttp;
                mensaje = excepcionBase.Message;
                descripcion = excepcionBase.Descripcion;

                respuesta.ContentType = "application/json";
                respuesta.StatusCode = estatusCodigo;

                    await respuesta.WriteAsync(JsonConvert.SerializeObject(new ExcepcionRespuestaEntidad
                    {
                        Error = mensaje
                    }));

            }

        }

        private async Task HandleInternalServerErrorExceptionAsync(HttpContext context, Exception ex)
        {

            if (ex.Message != null || ex.Data != null)
            {
                var respuesta = context.Response;

                respuesta.ContentType = "application/json";
                respuesta.StatusCode = (int)HttpStatusCode.BadRequest;

                _logger.Error(ex.Message, ex.StackTrace);

              
                    await respuesta.WriteAsync(JsonConvert.SerializeObject(new ExcepcionRespuestaEntidad()
                    {
                        Error = "Problemas internos en el servidor, intentelo más tarde"
                    }));   
                }
            }
        }
    }


