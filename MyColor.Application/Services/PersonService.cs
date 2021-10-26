using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MyColor.Application.DTOs;
using MyColor.Application.Interfaces;
using MyColor.Domain.Entities;
using MyColor.Domain.Interfaces;

namespace MyColor.Application.Services
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            this._personRepository = personRepository ??
                throw new ArgumentNullException(nameof(personRepository));
            this._mapper = mapper;
        }
        
        public async Task<IEnumerable<PersonDTO>> GetPersonsAsync()
        {
            var personsEntity = await this._personRepository.GetPersonsAsync();
            return this._mapper.Map<IEnumerable<PersonDTO>>(personsEntity);
        }

        public async Task<PersonDTO> GetPersonByIdAsync(int? id)
        {
            var personEntity = await this._personRepository.GetPersonByIdAsync(id);
            return this._mapper.Map<PersonDTO>(personEntity);
        }

        public async Task<IEnumerable<PersonDTO>> GetPersonByColorAsync(string color)
        {
            //TODO: Verify the return value from personRepository (FirstOrDefault?)
            var personsEntity = await this._personRepository.GetPersonByColorAsync(ApplicationColors.GetColorIdByName(color));
            return this._mapper.Map<IEnumerable<PersonDTO>>(personsEntity);
        }

        public async Task AddAsync(PersonDTO personDto)
        {
            var personEntity = this._mapper.Map<Person>(personDto);
            await this._personRepository.CreateAsync(personEntity);
        }

        public async Task UpdateAsync(PersonDTO personDto)
        {
            var personEntity = this._mapper.Map<Person>(personDto);
            await this._personRepository.UpdateAsync(personEntity);
        }

        public async Task RemoveAsync(int? id)
        {
            var personEntity = this._personRepository.GetPersonByIdAsync(id).Result;
            await this._personRepository.RemoveAsync(personEntity);
        }
    }
}
