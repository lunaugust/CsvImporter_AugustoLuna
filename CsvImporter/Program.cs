using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CsvImporter
{

    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console(LogEventLevel.Debug)
                .CreateLogger();

            try
            {
                Log.Information("Csv Importer Started");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Something went wrong");
            }
            finally
            {
                Log.Information(messageTemplate: "Csv Importer Stopped");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureHostConfiguration(config =>
                //{
                //    config.SetBasePath(Directory.GetCurrentDirectory());
                //    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                //})
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .UseSerilog();
    }

}
