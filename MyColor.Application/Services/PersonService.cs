using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MyColor.Application.ApplicationException;
using MyColor.Application.DTOs;
using MyColor.Application.Interfaces;
using MyColor.Domain.Entities;
using MyColor.Domain.Interfaces;
using MyColor.Domain.Utils;
using MyColor.Domain.Validation;

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

        public async Task<PersonDTO> AddAsync(PersonDTO personDto)
        {
            if (personDto.Id != 0)
            {
                Person p = await this._personRepository.GetPersonByIdAsync(personDto.Id);

                if (p != null)
                    throw new AppServiceException($"Person cannot be insert. The id {p.Id} already exists.");
            }
            try
            {
                var personEntity = this._mapper.Map<Person>(personDto);
                personDto = this._mapper.Map<PersonDTO>(await this._personRepository.CreateAsync(personEntity));
                return personDto;
            }
            catch(Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;
                else
                    throw new Exception(e.Message);
            }
        }

        public async Task UpdateAsync(PersonDTO personDto)
        {
            try
            {
                var personEntity = this._mapper.Map<Person>(personDto);
                await this._personRepository.UpdateAsync(personEntity);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;
                else
                    throw new Exception(e.Message);
            }
        }

        public async Task RemoveAsync(int? id)
        {
            var personEntity = this._personRepository.GetPersonByIdAsync(id).Result;
            await this._personRepository.RemoveAsync(personEntity);
        }
    }
}
