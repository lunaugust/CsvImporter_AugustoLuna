using AutoMapper;
using CsvHelper;
using CsvImporter.Data;
using CsvImporter.Domain;
using CsvImporter.Mappings;
using CsvImporter.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CsvImporter.Services
{
    public class CsvImporterService : ICsvImporterService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly ICsvImporterRepository _csvImporterRepository;

        public CsvImporterService(ILogger<Worker> logger, HttpClient httpClient, IMapper mapper, ICsvImporterRepository csvImporterRepository)
        {
            _logger = logger;
            _httpClient = httpClient;
            _mapper = mapper;
            _csvImporterRepository = csvImporterRepository;
        }

        public async Task ClearPreviousDataAsync()
        {
            await _csvImporterRepository.TruncateTableAsync<Sale>();
            _logger.LogInformation("Previous data were cleaned");
        }

        public async Task ImportAsync(string url, int batchSize)
        {
            using (var stream = await _httpClient.GetStreamAsync(url))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<SaleDtoMap>();
                csv.Configuration.Delimiter = ";";
                csv.Configuration.BadDataFound = null;

                var tempList = new List<SaleDto>();

                while (csv.Read())
                {
                    tempList.Add(csv.GetRecord<SaleDto>());

                    if (tempList.Count >= batchSize)
                    {
                        _logger.LogInformation("Batch loaded with {batchSize}", batchSize);
                        try
                        {
                            var sales = _mapper.Map<List<Sale>>(tempList);
                            _csvImporterRepository.BulkInsert(sales);
                            _logger.LogInformation("Batch saved.");
                            tempList.Clear();
                        }
                        catch (System.Exception ex)
                        {
                            _logger.LogError(ex, "Something went wrong");
                            throw;
                        }
                    }
                }
                _logger.LogInformation("Csv imported successfully.");
            }
        }
    }
}
