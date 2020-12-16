using CsvHelper.Configuration;
using CsvImporter.Models;
using System.Globalization;

namespace CsvImporter.Mappings
{
    public sealed class SaleDtoMap : ClassMap<SaleDto>
    {
        public SaleDtoMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.PointOfSale).Name("PointOfSale");
            Map(m => m.Product).Name("Product");
            Map(m => m.Date).Name("Date");
            Map(m => m.Stock).Name("Stock");
        }
    }
}
