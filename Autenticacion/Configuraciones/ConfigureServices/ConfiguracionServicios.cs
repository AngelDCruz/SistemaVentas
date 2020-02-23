using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;
using Autenticacion.Dominio.Repositorio;
using Autenticacion.Infraestructura.Repositorio;
using Autenticacion.Api.Servicios.Usuarios;
using Autenticacion.Dominio.Servicios.Roles;
using Autenticacion.Dominio.Entidades;
using Autenticacion.Infraestructura;
using Autenticacion.Dominio.Servicios.Autenticacion;
using Autenticacion.Dominio.Servicios.Cuenta;

using Serilog;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System;
using System.Linq;
using Autenticacion.Dominio.Swagger;

namespace Autenticacion.Api.Startup.ConfigureServices
{
    public static class ConfiguracionServicios
    {

        public static IServiceCollection Extenciones(this IServiceCollection services, IConfiguration configuration)
        {

      

            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            Identity(services);

            ConexionSqlServer(services, configuration);

            Repositorios(services);

            Servicios(services);

            SeriLog(services, configuration);

            JWTAutenticacion(services, configuration);

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
            services.AddScoped<ITokenRepositorio, TokenRepositorio>();
            services.AddScoped<ICuentaRepositorio, CuentaRepositorio>();

            return services;

        }

        //SERVICIOS Y ABSTRACCIONES
        private static IServiceCollection Servicios(this IServiceCollection services)
        {

    

            services.AddScoped<IUsuariosServicios, UsuariosServicios>();
            services.AddScoped<IRolesServicios, RolesServicios>();
            services.AddScoped<IAutenticacionServicios, AutenticacionServicios>();
            services.AddScoped<ICuentaServicios, CuentaServicios>();

            return services;

        }

        private static IServiceCollection SeriLog(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<Serilog.ILogger>(options => {
                return new LoggerConfiguration()
                       .WriteTo
                       .MSSqlServer(configuration.GetConnectionString("Autenticacion"),
                                    configuration["Serilog"],
                                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning,
                                    autoCreateSqlTable: true
                                    //columnOptions: opcionesColumna
                                    )
                       .CreateLogger();
            });

            return services;

        }

        private static IServiceCollection JWTAutenticacion(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var tokenValidationParameters = new TokenValidationParameters()
            {

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JWT:Secret"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Issuer"],
                RequireExpirationTime = false,
                ValidateLifetime = true

            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x =>
            {

                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });


            services.AddAuthorization();

            return services;

        }

        private static IServiceCollection Swagger(this IServiceCollection services)
        {


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
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                configuracion.IncludeXmlComments(xmlPath);

            });

            return services;

        } 

    }
}
