using MyColor.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyColor.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetPersonsAsync();
        Task<Person> GetPersonByIdAsync(int? id);
        Task<IEnumerable<Person>> GetPersonByColorAsync(int? color);
        Task<Person> CreateAsync(Person person);
        Task<Person> UpdateAsync(Person person);
        Task<Person> RemoveAsync(Person person);
    }
}
