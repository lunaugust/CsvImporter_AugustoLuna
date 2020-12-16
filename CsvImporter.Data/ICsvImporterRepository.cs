using CsvImporter.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvImporter.Data
{
    public interface ICsvImporterRepository
    {
        void RunMigrations();

        void BulkInsert(IList<Sale> entities);

        Task TruncateTableAsync<T>();

        Task SaveChangesAsync();
    }
}