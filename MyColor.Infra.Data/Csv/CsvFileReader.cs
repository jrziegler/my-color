using CsvHelper;
using CsvHelper.Configuration;
using MyColor.Domain.Entities;
using MyColor.Infra.Data.Interfaces;
using MyColor.Infra.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MyColor.Infra.Data.Csv
{
    public class CsvFileReader : IFileReader
    {
        private const string FILE_NAME = "sample-input.csv";
        private const string FILE_PATH = "../MyColor.Infra.Data/Csv/";

        public CsvFileReader()
        { }

        public IEnumerable<Person> LoadDataFromFile()
        {
            List<Person> listOfPersons = new();
            List<string> listOfBadRecords = new();

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                TrimOptions = TrimOptions.Trim,
                MissingFieldFound = null,
                IgnoreBlankLines = true,
                LineBreakInQuotedFieldIsBadData = true,
            };

            try
            {
                using (StreamReader reader = new(Path.Combine(Environment.CurrentDirectory, $"{FILE_PATH}{FILE_NAME}")))
                {
                    using (CsvReader csv = new(reader, config))
                    {
                        csv.Context.RegisterClassMap<CsvToPersonFromCsvMappingProfile>();

                        var records = csv.GetRecords<PersonFromCsv>();

                        foreach (PersonFromCsv r in records)
                        {
                            string[] record = r.ToCsv().Split(",");
                            if (string.IsNullOrWhiteSpace(record.ElementAt(0)) ||
                                string.IsNullOrWhiteSpace(record.ElementAt(1)) ||
                                string.IsNullOrWhiteSpace(record.ElementAt(2)) ||
                                string.IsNullOrWhiteSpace(record.ElementAt(3)))
                            {
                                listOfBadRecords.Add($"RowNo: {csv.Context.Parser.RawRow}, Record: {csv.Parser.RawRecord}");
                            }
                            else
                            {
                                Person p = new(
                                        csv.Context.Parser.RawRow,
                                        r.Name,
                                        r.LastName,
                                        r.GetZipcode(),
                                        r.GetCityName(),
                                        (int)r.Color
                                    );

                                listOfPersons.Add(p);
                            }
                        }
                    }
                }
                return listOfPersons;
            }
            catch (Exception e)
            {
                //TODO: Error in Log
                throw new Exception(e.Message);
            }
            finally
            {
                if (listOfBadRecords.Any())
                    //TODO: list of bad records inside a log
                    listOfBadRecords.Count();
            }
        }
    }
}