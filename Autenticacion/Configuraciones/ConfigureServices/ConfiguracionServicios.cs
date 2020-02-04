using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Autenticacion.Infraestructura;
using Autenticacion.Dominio.Entidades;
using Autenticacion.Dominio.Repositorio.Contratos;
using Autenticacion.Infraestructura.Repositorio;
using Autenticacion.Api.Servicios;
using Microsoft.AspNetCore.Http;

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;

        }

        //IDENTITY
        private static IServiceCollection Identity (this IServiceCollection services)
        {

            services.AddIdentity<Usuarios, Roles>(opciones => {


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

                options.UseSqlServer(configuration.GetConnectionString("Autenticacion"));

            });

            return services;

        }


        //REPOSITORIOS Y ABSTRACCIONES
        private static IServiceCollection Repositorios(IServiceCollection services)
        {

            services.AddScoped<IUsuariosInformacionRepositorio, UsuariosRepositorio>();
            services.AddScoped<IUsuariosProcesoRepositorio, UsuariosRepositorio>();

            return services;

        }

        //SERVICIOS Y ABSTRACCIONES
        private static IServiceCollection Servicios(this IServiceCollection services)
        {

            services.AddScoped<IUsuariosServicios, UsuariosServicios>();

            return services;

        }

        private static IServiceCollection Automapper(this IServiceCollection services)
        {

   
           return services;

        }
    }
}
