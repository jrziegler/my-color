using MyColor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyColor.Domain.Interfaces
{
    public interface PersonRepository
    {
        Task<IEnumerable<Person>> GetPersonsAsync();
        Task<Person> GetPersonByIdAsync(int? id);
        Task<IEnumerable<Person>> GetPersonByColorAsync(string? color);
        Task<Person> CreateAsync(Person person);
        Task<Person> UpdateAsync(Person person);
        Task<Person> RemoveAsync(Person person);
    }
}
