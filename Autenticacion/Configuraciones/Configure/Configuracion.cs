﻿using Autenticacion.Infraestructura;
using Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SistemaVentas.Infraestructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Api.Startup.Configure
{
    public static class Configuracion
    {
        public static IApplicationBuilder Extenciones(this IApplicationBuilder app, IHostingEnvironment env,  AppDbContext context)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();


            }


            app.UseStaticFiles();

            app.UseAuthentication();

            Cors(app);

            MiddlewarezPersonalizados(app);

             Swagger(app);

            app.UseMvc();

            InicializarDatos.CrearDatos(context);

            return app;

        }

        private static  IApplicationBuilder Cors(this IApplicationBuilder app)
        {

            app.UseCors(configuracion => configuracion.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            return app;

        }

        private static IApplicationBuilder MiddlewarezPersonalizados(this IApplicationBuilder app)
        {

            //app.UseMiddleware<ExcepcionesMiddleware>();

            return app;

        }

        private static IApplicationBuilder Swagger(this IApplicationBuilder app)
        {


            app.UseSwagger();

            app.UseSwaggerUI(configuracion =>
            {

                configuracion.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1");

            });

            return app;
        }

    }
}
