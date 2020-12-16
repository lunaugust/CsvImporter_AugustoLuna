using CsvImporter.Domain;
using Microsoft.EntityFrameworkCore;

namespace CsvImporter.Data
{
    public class CsvImporterContext : DbContext
    {
        public CsvImporterContext(DbContextOptions<CsvImporterContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Sale> Sales { get; set; }
    }
}
