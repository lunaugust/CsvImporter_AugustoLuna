using CsvImporter.Data;
using CsvImporter.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImporter
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICsvImporterService _csvImporterService;
        private readonly ICsvImporterRepository _csvImporterRepository;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, ICsvImporterService csvImporterService, ICsvImporterRepository csvImporterRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _csvImporterService = csvImporterService;
            _csvImporterRepository = csvImporterRepository;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _csvImporterRepository.RunMigrations();
            await _csvImporterService.ClearPreviousDataAsync();
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var url = _configuration.GetValue<string>("Url");
                var batchSize = _configuration.GetValue<int>("BatchSize");
               
                await _csvImporterService.ImportAsync(url, batchSize);
            }
        }
    }
}
