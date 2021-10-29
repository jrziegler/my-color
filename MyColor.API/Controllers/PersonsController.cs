using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyColor.Application.DTOs;
using MyColor.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyColor.API.Controllers
{
    [Route("api/persons/")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            this._personService = personService ??
                throw new ArgumentNullException(nameof(personService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> Get()
        {
            var persons = await this._personService.GetPersonsAsync();
            if(persons == null)
            {
                return NotFound("Persons not found.");
            }

            return Ok(persons);
        }

        [HttpGet("{id:int}", Name = "GetPerson")]
        public async Task<ActionResult<PersonDTO>> Get(int id)
        {
            var person = await this._personService.GetPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound("Person not found.");
            }

            return Ok(person);
        }
 
        [HttpGet("color/{color}")]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> Get(string color)
        {
            var persons = await this._personService.GetPersonByColorAsync(color);
            if (!persons.Any())
            {
                return NotFound("Persons not found.");
            }

            return Ok(persons);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PersonDTO personDto)
        {
            if (personDto == null)
                return BadRequest("Invalid data.");

            try
            {
                personDto = await _personService.AddAsync(personDto);
                return new CreatedAtRouteResult("GetPerson", new { id = personDto.Id }, personDto);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] PersonDTO personDto)
        {
            if (id != personDto.Id)
                return BadRequest("The field id does not conform with requested in the body.");

            if (personDto == null)
                return BadRequest("Invalid data.");

            try
            {
                await _personService.UpdateAsync(personDto);
                return Ok(personDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var personDto = await this._personService.GetPersonByIdAsync(id);

            if (personDto == null)
                return BadRequest("Person not found.");

            await this._personService.RemoveAsync(id);

            return Ok(personDto);
        }
    }
}
