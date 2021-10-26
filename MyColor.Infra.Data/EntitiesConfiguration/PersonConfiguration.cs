using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyColor.Domain.Entities;
using MyColor.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MyColor.Infra.Data.EntitiesConfiguration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.ZipCode).HasMaxLength(10);
            builder.Property(p => p.City).HasMaxLength(100);

            //builder.HasData(SeedDataFromCsv());
            //builder.HasData(SeedDataTest());
        }

        private static IEnumerable<Person> SeedDataTest()
        {
            List<Person> people = new List<Person>();
            people.Add(
                new Person(
                            1,
                            "Tony",
                            "Stark",
                            "10200",
                            "California",
                            4
                )
            );
            people.Add(
                new Person(
                            2,
                            "Steve",
                            "Rogers",
                            "10201",
                            "California",
                            1
                )
            );

            return people;
        }

        private async void SeedDataFromCsv()
        {
            /*
            List<Person> people = new List<Person>();
            people.Add(
                new Person(
                            1,
                            "Tony",
                            "Stark",
                            "10200",
                            "California",
                            4
                )
            );
            people.Add(
                new Person(
                            2,
                            "Steve",
                            "Rogers",
                            "10201",
                            "California",
                            1
                )
            );
            */
            //var people = LoadFromCsv(Path.Combine(Environment.CurrentDirectory, $"../MyColor.Infra.Data/Repositories/sample-input.csv"));
            var people = ReadPersonsFromFile();
/*
            foreach (Person p in people)
            {
                _personContext.Add(p);
            }
            await _personContext.SaveChangesAsync();*/
        }

        /**********************************************************************/

        private IEnumerable<Person> ReadPersonsFromFile()
        {
            var results = new List<Person>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                TrimOptions = TrimOptions.Trim,
                MissingFieldFound = null,
                IgnoreBlankLines = true,
                LineBreakInQuotedFieldIsBadData = true,

            };

            try
            {
                using (var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, $"../MyColor.Infra.Data/Repositories/sample-input.csv")))
                {
                    using (var csv = new CsvReader(reader, config))
                    {
                        var records = csv.GetRecords<PersonFromCsv>();

                        foreach (PersonFromCsv r in records)
                        {
                            string[] record = r.ToCsv().Split(",");
                            if (record.Length < 4)
                            {
                                //TODO: Make a better exception handler 
                                //throw new Exception($"Row {csv.Context.Parser.RawRow} with content: {record.ToString()} does not conform to record type.");
                            }
                            if (string.IsNullOrWhiteSpace(record.ElementAt(2)) || string.IsNullOrWhiteSpace(record.ElementAt(3)))
                            {
                                // TODO: Make a better exception handler
                                //throw new Exception($"Row {csv.Context.Parser.RawRow} with content: {record.ToString()} does not conform to record type.");
                            }
                            else
                            {
                                Person p = new Person(
                                        csv.Context.Parser.RawRow,
                                        r.Name,
                                        r.LastName,
                                        r.GetZipcode(),
                                        r.GetCityName(),
                                        (int)r.Color
                                    );
                                results.Add(p);
                            }
                        }
                    }
                }
                return results;
            }
            catch (Exception e)
            {
                //TODO: Error in Log
                throw new Exception(e.Message);
            }
        }
    }
}
