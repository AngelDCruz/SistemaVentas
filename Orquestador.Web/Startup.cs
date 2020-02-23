using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Orquestador.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddOcelot();

            services.AddSwaggerGen(configuracion =>
            {

                configuracion.SwaggerDoc("v1", new OpenApiInfo
                {

                    Title = "DOCUMENTACION",
                    Description = "Documentacion de recursos expuestos para consumo del cliente",
                    Contact = new OpenApiContact
                    {
                        Name = "Angel Reynaldo Ortiz De La Cruz",
                        Email = "aldo_amor-@hotmail.com",
                        Url = new Uri("https://www.facebook.com/angelus.ocrz")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "SIA SOFTWARE",
                        Url = new Uri("https://www.siasw.com/index.php/es/")
                    }

                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                configuracion.IncludeXmlComments(xmlPath);

            });

            services.AddSwaggerForOcelot(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            app.UseSwagger();

            app.UseSwaggerUI(configuracion =>
            {
                configuracion.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseSwaggerForOcelotUI(Configuration, opt =>
            {

                opt.PathToSwaggerGenerator = "/swagger/docs";

            });


            app.UseHttpsRedirection();

            app.UseMvc();

            app.UseOcelot().Wait();

        }
    }
}
