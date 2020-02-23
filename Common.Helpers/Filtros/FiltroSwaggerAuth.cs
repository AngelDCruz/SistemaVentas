using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Filtros
{
    public class FiltroSwaggerAuth : IOperationFilter
    {

        /// <summary>
        /// SE APLICA A TODOS LOS ENDPOINTS DE LOS REST
        /// NOTA: ESTO SE APLICA A SWAGGER ESPECIFICAMENTE EN AddSwaggerGen
        /// AL AGREGAR EL METODO DENTRO PONER LO SIGUIENTE
        ///        services.AddSwaggerGen(c =>
        ///     {
        //          c.OperationFilter<CustomHeaderSwaggerAttribute>();
        //      }
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
         
            // VALIDA SI SON NULLOS 
            if(operation.Parameters == null)
            {

                // CREA LISTA DE POSIBLES PARAMETROS

                operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter {

                    Name = "grant_type",
                    In = ParameterLocation.Header,
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "String"
                    }

                });

            }

        }
    }
}
