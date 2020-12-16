using CsvImporter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CsvImporter.Configuration
{
    public class DbContextFactory : IDesignTimeDbContextFactory<CsvImporterContext>
    {
        public CsvImporterContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
            
            var optionsBuilder = new DbContextOptionsBuilder<CsvImporterContext>();
            optionsBuilder
                .UseSqlServer(configuration.GetConnectionString("CsvImporterConnectionString"))
                .EnableSensitiveDataLogging(true);

            return new CsvImporterContext(optionsBuilder.Options);
        }
    }
}
