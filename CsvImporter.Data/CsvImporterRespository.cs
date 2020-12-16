using CsvImporter.Domain;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvImporter.Data
{
    public class CsvImporterRespository : ICsvImporterRepository
    {
        private readonly CsvImporterContext _context;

        public CsvImporterRespository(CsvImporterContext context)
        {
            _context = context;
        }
        
        public void RunMigrations()
        {
            _context.Database.Migrate();
        }

        public void BulkInsert(IList<Sale> entities)
        {
            _context.BulkInsert(entities);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task TruncateTableAsync<T>()
        {
            await _context.TruncateAsync(typeof(T));
        }
    }
}
