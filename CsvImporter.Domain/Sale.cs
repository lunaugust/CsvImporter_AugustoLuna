using System;

namespace CsvImporter.Domain
{
    public class Sale
    {
        public int Id { get; set; }

        public string PointOfSale { get; set; }

        public string Product { get; set; }

        public DateTime Date { get; set; }

        public int Stock { get; set; }
    }
}
