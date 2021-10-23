using Microsoft.EntityFrameworkCore;
using MyColor.Domain.Entities;
using MyColor.Domain.Interfaces;
using MyColor.Infra.Data.Context;
using System.Collections.Generic;
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
    }
}
