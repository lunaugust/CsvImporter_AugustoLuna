using System;

namespace CsvImporter.Models
{
    public class SaleDto
    {
        public string PointOfSale { get; set; }

        public string Product { get; set; }

        public DateTime Date { get; set; }

        public int Stock { get; set; }
    }
}
