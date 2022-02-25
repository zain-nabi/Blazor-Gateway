using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Gateway.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == Environments.Development;

            CreateHostBuilder(args)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                if (isDevelopment)
                {
                    config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    //.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    .AddJsonFile("ocelot.Development.json")
                    .AddEnvironmentVariables();
                }
                else
                {
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        //.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                        .AddJsonFile("ocelot.json")
                        .AddEnvironmentVariables();
                }

            })
            .ConfigureServices(s =>
            {
                s.AddOcelot();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                //add your logging
            })
           //.Configure(app =>
           //{
           //    app.UseOcelot().Wait();
           //})
           .Build().Run();
            //new WebHostBuilder()
            //               .UseKestrel()
            //               .UseContentRoot(Directory.GetCurrentDirectory())
            //               .ConfigureAppConfiguration((hostingContext, config) =>
            //               {
            //                   config
            //                       .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            //                       .AddJsonFile("appsettings.json", true, true)
            //                       .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
            //                       .AddJsonFile("ocelot.json")
            //                       //.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json")
            //                       .AddEnvironmentVariables();
            //               })
            //               .ConfigureServices(s =>
            //               {
            //                   s.AddOcelot();
            //               })
            //               .ConfigureLogging((hostingContext, logging) =>
            //               {
            //                   //add your logging
            //               })
            //               .UseIISIntegration()
            //               .Configure(app =>
            //               {
            //                   app.UseOcelot().Wait();
            //               })
            //               .UseUrls("http://localhost:7000")
            //               .Build()
            //               .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseUrls("http://localhost:7000/");
                });
    }
}
