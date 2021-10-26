using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using MyColor.Domain.Entities;
using MyColor.Domain.Interfaces;
using MyColor.Infra.Data.Context;
using MyColor.Infra.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyColor.Infra.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        ApplicationDbContext _personContext;

        public PersonRepository(ApplicationDbContext context)
        {
            this._personContext = context;
            //SeedDataTest();
        }

        public async Task<Person> CreateAsync(Person person)
        {
            this._personContext.Add(person);
            await this._personContext.SaveChangesAsync();
            return person;
        }

        public async Task<IEnumerable<Person>> GetPersonByColorAsync(int? color)
        {
            return await this._personContext.Persons.Where(p => p.Color == color).ToListAsync();
        }

        public async Task<Person> GetPersonByIdAsync(int? id)
        {
            return await this._personContext.Persons.FindAsync(id);
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync()
        {
            return await this._personContext.Persons.ToListAsync();
        }

        public async Task<Person> RemoveAsync(Person person)
        {
            this._personContext.Remove(person);
            await this._personContext.SaveChangesAsync();
            return person;
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            this._personContext.Update(person);
            await this._personContext.SaveChangesAsync();
            return person;
        }

        private async void SeedDataTest()
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

            foreach (Person p in people)
            {
                _personContext.Add(p);
            }
            await _personContext.SaveChangesAsync();
        }

        /**********************************************************************/

        private IEnumerable<Person> LoadFromCsv(string path)
        {
            try
            {
                using var csv = new CsvFileReader(path, ",", false);
                var persons = new List<Person>();
                var counter = 1;
                while (csv.Read())
                {
                    string[] record = csv.ToString().Split(","); //.ToCsv().Split(",");
                    if (record.Length < 4)
                    {
                        //TODO: Make a better exception handler 
                        //throw new Exception($"Row {csv.Context.Parser.RawRow} with content: {record.ToString()} does not conform to record type.");
                    }

                    var personFromCsv = new PersonFromCsv
                    {
                        Name = csv.GetIndex(1).Trim(),
                        LastName = csv.GetIndex(0).Trim()
                    };

                    var completeAdress = csv.GetIndex(2).Trim();
                    if (string.IsNullOrEmpty(completeAdress))
                    {
                        break;
                    }
                    personFromCsv.CompleteAdress = completeAdress;

                    var color = csv.GetIndex(3).Trim();
                    if (string.IsNullOrEmpty(color))
                    {
                        break;
                    }
                    personFromCsv.Color = Convert.ToInt32(color);

                    Person person = new (
                            counter++,
                            personFromCsv.Name,
                            personFromCsv.LastName,
                            personFromCsv.CompleteAdress.Substring(0, personFromCsv.CompleteAdress.IndexOf(" ")),
                                        personFromCsv.CompleteAdress.Substring(personFromCsv.CompleteAdress.IndexOf(" ") + 1, personFromCsv.CompleteAdress.Length - (personFromCsv.CompleteAdress.IndexOf(" ") + 1)),
                            personFromCsv.Color ?? 0
                        ); 
                    persons.Add(person);

                    //counter++;
                }

                return persons;
            }
            catch (Exception e)
            {
                //TODO: Error in Log
                throw new Exception(e.Message);
            }
        }

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
                        //csv.Context.RegisterClassMap<CsvToPersonFromCsvMappingProfile>();
                        /*config.BadDataFound = context =>
                        {
                            isBadRecord = true;
                            bad.Add(context.RawRecord);
                        };

                        while (csv.Read())
                        {
                            var record = csv.GetRecord<PersonFromCsv>();
                            if (!isBadRecord)
                            {
                                good.Add(record);
                            }

                            isBadRecord = false;
                        }*/

                        //good.Dump();
                        //bad.Dump();

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
            catch(Exception e)
            {
                //TODO: Error in Log
                throw new Exception(e.Message);
            }
        }
    }
}
