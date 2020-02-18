using Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Api.Startup.Configure
{
    public static class Configuracion
    {
        public static IApplicationBuilder Extenciones(this IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseAuthentication();

            MiddlewarezPersonalizados(app);

            app.UseMvc();

            return app;

        }

        private static IApplicationBuilder MiddlewarezPersonalizados(this IApplicationBuilder app)
        {

            //app.UseMiddleware<ExcepcionesMiddleware>();

            return app;

        }

    }
}
