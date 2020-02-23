using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace Orquestador.Microservicio
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



            ////CONFIGURACION SWAGGER
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


            app.UseSwagger();

            app.UseSwaggerUI(configuracion =>
            {

                configuracion.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1 API REST");

            });

            app.UseSwaggerForOcelotUI(Configuration, opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            app.UseOcelot().Wait();

            app.UseMvc();

        }
    }
}
