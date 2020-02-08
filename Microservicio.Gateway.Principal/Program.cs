
using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using Microsoft.AspNetCore;

namespace Microservicio.Gateway.Principal
{
    public class Program
    {

        public static void Main(string[] args)
        {

            CreateWebHostBuilder(args).Build().Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
              .ConfigureAppConfiguration((hostingContext, config) =>
              {
                  config
                       .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                       .AddJsonFile("appsettings.json", true, true)
                       .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                      //.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                       .AddOcelot(hostingContext.HostingEnvironment)
                       .AddEnvironmentVariables();

             
              })
              .UseStartup<Startup>();



    }
}
