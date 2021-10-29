using System.Collections.Generic;
using System.Threading.Tasks;
using MyColor.Application.DTOs;

namespace MyColor.Application.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDTO>> GetPersonsAsync();
        Task<PersonDTO> GetPersonByIdAsync(int? id);
        Task<IEnumerable<PersonDTO>> GetPersonByColorAsync(string color);
        Task<PersonDTO> AddAsync(PersonDTO personDto);
        Task UpdateAsync(PersonDTO personDto);
        Task RemoveAsync(int? id);
    }
}
