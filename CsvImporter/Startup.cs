using AutoMapper;
using CsvImporter.Configuration;
using CsvImporter.Data;
using CsvImporter.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace CsvImporter
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
            services.AddDbContext<CsvImporterContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("CsvImporterConnectionString"))
                .EnableSensitiveDataLogging());
            services.AddScoped<ICsvImporterRepository, CsvImporterRespository>();
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddScoped<ICsvImporterService, CsvImporterService>();
            services.AddSingleton<HttpClient>();
            services.AddHostedService<Worker>();
        }

        public void Configure()
        {
        }
    }
}
