using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace Microservicio.Gateway.Principal
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

            services.AddCors();

            services.AddOcelot()
                    .AddConsul();

            services.AddSwaggerForOcelot(Configuration);

            //CONFIGURACION SWAGGER
            services.AddSwaggerGen(configuracion => {


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

                configuracion.SwaggerDoc("v2", new OpenApiInfo
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();

            app.UseSwaggerUI(configuracion => {

                configuracion.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1 API REST");
                configuracion.SwaggerEndpoint("/swagger/v2/swagger.json", "Version 2 API REST");

            });

            app.UseSwaggerForOcelotUI(Configuration, opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });

            app.UseOcelot().Wait();

            app.UseMvc();

        }
    }
}
