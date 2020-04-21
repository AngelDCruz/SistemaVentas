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

using SistemaVentas.Dominio.Repositorio;
using SistemaVentas.Infraestructura.Repositorio;
using SistemaVentas.Dominio.Servicios.Categoria;
using SistemaVentas.Dominio.Servicios.Productos;
using System.Collections.Generic;
using SistemaVentas.Dominio.Servicios.Clientes;
using SistemaVentas.Dominio.Servicios.Proveedores;
using SistemaVentas.Dominio.Servicios.Ingresos;

namespace Autenticacion.Api.Startup.ConfigureServices
{
    public static class ConfiguracionServicios
    {

        public static IServiceCollection Extenciones(this IServiceCollection services, IConfiguration configuration)
        {

      

            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            Identity(services);

            ConexionSqlServer(services, configuration);

            Cors(services);

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

        // CORS
        private static IServiceCollection Cors(this IServiceCollection services)
        {

            services.AddCors();

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
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            return services;

        }

        //CONECTOR DE BASE DE DATOS
        private static IServiceCollection ConexionSqlServer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
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
            services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
            services.AddScoped<IProductosRepositorio, ProductosRepositorio>();
            services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
            services.AddScoped<IIngresoRepositorio, IngresoRepositorio>();
            services.AddScoped<IDetallesIngresoRepositorio, DetalleIngresoRepositorio>();


            return services;

        }

        //SERVICIOS Y ABSTRACCIONES
        private static IServiceCollection Servicios(this IServiceCollection services)
        {

            services.AddScoped<IUsuariosServicios, UsuariosServicios>();
            services.AddScoped<IRolesServicios, RolesServicios>();
            services.AddScoped<IAutenticacionServicios, AutenticacionServicios>();
            services.AddScoped<ICuentaServicios, CuentaServicios>();
            services.AddScoped<IProductosServicios, ProductosServicios>();
            services.AddScoped<ICategoriaServicios, CategoriaServicios>();
            services.AddScoped<IClientesServicios, ClientesServicios>();
            services.AddScoped<IProveedoresServicios, ProveedoresServicios>();
            services.AddScoped<IIngresosServicios, IngresosServicios>();

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

                // CONFIGURACION SEGURIDAD
                configuracion.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {

                    Name = "Authorization",
                    Description = "Autorización de esquemas utilizando el portador jwt",
                    In =  ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey

                });

                configuracion.AddSecurityRequirement(new OpenApiSecurityRequirement
                {

                    {

                        new OpenApiSecurityScheme
                        {

                            Reference = new OpenApiReference
                            {

                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme

                            }

                        }, new List<string>()

                    }

                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                configuracion.IncludeXmlComments(xmlPath);

            });

            return services;

        } 

    }
}
