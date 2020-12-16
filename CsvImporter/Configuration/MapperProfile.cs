using AutoMapper;
using CsvImporter.Domain;
using CsvImporter.Models;

namespace CsvImporter.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<SaleDto, Sale>()
              .ReverseMap();
        }
    }
}
