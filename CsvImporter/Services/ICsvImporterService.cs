using System.Threading.Tasks;

namespace CsvImporter.Services
{
    public interface ICsvImporterService
    {
        Task ImportAsync(string url, int batchSize);

        Task ClearPreviousDataAsync();
    }
}
