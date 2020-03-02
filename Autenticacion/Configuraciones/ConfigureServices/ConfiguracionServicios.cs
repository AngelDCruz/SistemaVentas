using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using Autenticacion.Api.Servicios.Usuarios;



using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System;
using System.Collections.Generic;
using SistemaVentas.Api.Servicios.Usuarios;
using SistemaVentas.Dominio.Servicios.Roles;
using SistemaVentas.Dominio.Repositorio.Contratos;
using SistemaVentas.Infraestructura.Repositorio;
using SistemaVentas.Dominio.Entidades;
using SistemaVentas.Infraestructura;

namespace Autenticacion.Api.Startup.ConfigureServices
{
    public static class ConfiguracionServicios
    {

        public static IServiceCollection Extenciones(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            Identity(services);

            ConexionSqlServer(services, configuration);

            Repositorios(services);

            Servicios(services);

            Automapper(services);

            Swagger(services);

            services.AddMvc()
             .AddJsonOptions(configuraciones => configuraciones.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;

        }

        //IDENTITY
        private static IServiceCollection Identity (this IServiceCollection services)
        {

            services.AddIdentity<UsuariosEntidad, RolesEntidad>(opciones => {


                opciones.Password.RequireDigit = false;
                opciones.Password.RequireNonAlphanumeric = false;
                opciones.Password.RequireUppercase = false;
                opciones.Password.RequireLowercase = false;

            })
            //.AddUserStore<Usuarios>()
            //.AddRoleStore<Roles>()
            //.AddUserManager<Usuarios>()
            //.AddRoleManager<Roles>()
            .AddEntityFrameworkStores<AutenticacionDbContext>()
            .AddDefaultTokenProviders();

            return services;

        }

        //CONECTOR DE BASE DE DATOS
        private static IServiceCollection ConexionSqlServer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AutenticacionDbContext>(options =>
            {

                //options.UseLazyLoadingProxies();

                options.UseSqlServer(configuration.GetConnectionString("Autenticacion"), 
                                     builder => builder.UseRowNumberForPaging());

            });

            return services;

        }

        //REPOSITORIOS Y ABSTRACCIONES
        private static IServiceCollection Repositorios(IServiceCollection services)
        {

            services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();
          
            services.AddScoped<IRolesRepositorio, RolesRepositorio>();

            services.AddScoped<IUsuariosRolesRepositorio, UsuariosRolesRepositorio>();

            return services;

        }

        //SERVICIOS Y ABSTRACCIONES
        private static IServiceCollection Servicios(this IServiceCollection services)
        {

            services.AddScoped<IUsuariosServicios, UsuariosServicios>();
            services.AddScoped<IRolesServicios, RolesServicios>();
            

            return services;

        }

        private static IServiceCollection Automapper(this IServiceCollection services)
        {

   
           return services;

        }

        private static IServiceCollection Swagger(this IServiceCollection services)
        {

            //REGISTRO DE DEFINICIONES DE VERSIONES DE API
            services.AddSwaggerGen(configuracion =>
            {

                var authInformacion = new OpenApiInfo()
                {

                    Title = "Version v1",
                    Version = "v1",
                    Description = "Primeras versiones de api"

                };

                configuracion.SwaggerDoc("v1", authInformacion);

               
                //CONFIGURACION EN DOCUMENTACION MICROSOFT 
                //Estable documentacion para Swagger JSON Y UI
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                configuracion.IncludeXmlComments(xmlPath);


            });

            return services;

        }
    }
}
