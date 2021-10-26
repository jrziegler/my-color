using System;
using CsvHelper.Configuration.Attributes;

namespace MyColor.Infra.Data.Repositories
{
    public sealed class PersonFromCsv
    {
        [Index(1)]
        public string Name { get; set; }
        [Index(0)]
        public string LastName { get; set; }
        [Index(2)]
        [Optional]
        public string? CompleteAdress { get; set; }
        [Index(3)]
        [Optional]
        public int? Color { get; set; }

        public PersonFromCsv()
        { }

        public string ToCsv()
        {
            return $"{Name},{LastName},{CompleteAdress},{Color}";
        }

        public string GetZipcode()
        {
            if (this.CompleteAdress == null)
                return string.Empty;

            return CompleteAdress.Substring(0, CompleteAdress.IndexOf(" "));
        }

        public string GetCityName()
        {
            if (this.CompleteAdress == null)
                return string.Empty;

            return CompleteAdress.Substring(CompleteAdress.IndexOf(" ") + 1, CompleteAdress.Length - (CompleteAdress.IndexOf(" ") + 1));
        }
    }
}
