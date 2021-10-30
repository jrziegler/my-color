using CsvHelper.Configuration.Attributes;

namespace MyColor.Infra.Data.Csv
{
    public sealed class PersonFromCsv
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        [Optional]
        public string CompleteAdress { get; set; }
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
