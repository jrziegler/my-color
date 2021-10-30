using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MyColor.Application.ApplicationException;
using MyColor.Application.DTOs;
using MyColor.Application.Interfaces;
using MyColor.Application.Mappings;
using MyColor.Domain.Entities;
using MyColor.Domain.Interfaces;
using MyColor.Domain.Utils;

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
            Person personEntity = await this._personRepository.GetPersonByIdAsync(id);
            return this._mapper.Map<PersonDTO>(personEntity);
        }

        public async Task<IEnumerable<PersonDTO>> GetPersonByColorAsync(string color)
        {
            var personsEntity = await this._personRepository.GetPersonByColorAsync(ApplicationColors.GetColorIdByName(color));
            return this._mapper.Map<IEnumerable<PersonDTO>>(personsEntity);
        }

        public async Task<PersonDTO> AddAsync(PersonDTO personDto)
        {
            if (personDto.Id != 0)
            {
                Person p = await this._personRepository.GetPersonByIdAsync(personDto.Id);

                if (p != null)
                    throw new AppServiceException($"Person cannot be insert. The id {p.Id} already exists.");
            }

            Person personEntity = personDto.MapToDomain();
            personDto = this._mapper.Map<PersonDTO>(await this._personRepository.CreateAsync(personEntity));
            return personDto;
        }

        public async Task UpdateAsync(PersonDTO personDto)
        {
            Person personEntity = personDto.MapToDomain();
            await this._personRepository.UpdateAsync(personEntity);
        }

        public async Task RemoveAsync(int? id)
        {
            Person personEntity = this._personRepository.GetPersonByIdAsync(id).Result;
            await this._personRepository.RemoveAsync(personEntity);
        }
    }
}
