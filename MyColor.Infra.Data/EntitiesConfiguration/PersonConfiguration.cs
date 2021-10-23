using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyColor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            builder.HasData(SeedDataFromCsv());
        }

        private static IEnumerable<Person> SeedDataFromCsv()
        {
            string pathToCsvFile = string.Concat(Environment.CurrentDirectory, @"\Repositories\sample-input.csv");
            //TODO: try...catch
            using (var reader = new StreamReader(pathToCsvFile))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<Person>();
                }
            }
        }
    }
}
