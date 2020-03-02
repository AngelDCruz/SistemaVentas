
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

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


            app.UseMvc();

            //AUTENTICA
            app.UseAuthentication();

            SwaggerDocumentacion(app);

            return app;

        }

        public static IApplicationBuilder SwaggerDocumentacion(this IApplicationBuilder app)
        {

            app.UseSwagger();
        
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });


            return app;

        }

    }
}
