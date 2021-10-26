using System;
using CsvHelper.Configuration;
using MyColor.Infra.Data.Repositories;

namespace MyColor.Infra.Data.Mappings
{
    public sealed class CsvToPersonFromCsvMappingProfile : ClassMap<PersonFromCsv>
    {
        public CsvToPersonFromCsvMappingProfile()
        {
            Map(p => p.LastName).Index(0);
            Map(p => p.Name).Index(1);
            Map(p => p.CompleteAdress).Index(2);
            Map(p => p.Color).Index(3);
        }
    }
}
