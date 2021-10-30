using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MyColor.Domain.Entities;
using MyColor.Infra.Data.Interfaces;

namespace MyColor.Infra.Data.Context
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IFileReader _fileReader;

        public DbInitializer(IServiceScopeFactory scopeFactory, IFileReader fileReader)
        {
            this._scopeFactory = scopeFactory;
            this._fileReader = fileReader;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    if (!context.Persons.Any())
                    {
                        var people = _fileReader.LoadDataFromFile();
                        foreach (Person p in people)
                        {
                            context.Persons.Add(p);
                        }
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
