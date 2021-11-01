using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyColor.Application.DTOs;
using MyColor.Application.Interfaces;
using MyColor.Domain.Interfaces;

namespace MyColor.API.Controllers
{
    [ApiController]
    [Route("api/persons/")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILoggerService _logger;

        public PersonsController(IPersonService personService, ILoggerService logger)
        {
            this._personService = personService ??
                throw new ArgumentNullException(nameof(personService));

            this._logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Method used to get a list of persons.
        /// </summary>
        /// <returns>A list of persons with they favorite color.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> Get()
        {
            var persons = await this._personService.GetPersonsAsync();
            if (persons == null)
                throw new ArgumentException("Persons not found.");

            return Ok(persons);
        }

        /// <summary>
        /// Method used to get a person by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A person with his favorite color.</returns>
        [HttpGet("{id:int}", Name = "GetPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonDTO>> Get(int id)
        {
            var person = await this._personService.GetPersonByIdAsync(id);
            if (person == null)
                throw new ArgumentException("Person not found.");

            return Ok(person);
        }

        /// <summary>
        /// Method used to get a list of persons by color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns>A list of persons, which the favorite color is equal the parameter.</returns>
        [HttpGet("color/{color}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> Get(string color)
        {
            var persons = await this._personService.GetPersonByColorAsync(color);
            if (!persons.Any())
                throw new ArgumentException($"No persons with color {color} found.");

            return Ok(persons);
        }

        /// <summary>
        /// Method used to add a new person to the system.
        /// </summary>
        /// <param name="personDto"></param>
        /// <returns>The person created.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] PersonDTO personDto)
        {
            if (personDto == null)
                throw new ApplicationException("Invalid data.");

            personDto = await _personService.AddAsync(personDto);
            return new CreatedAtRouteResult("GetPerson", new { id = personDto.Id }, personDto);
        }

        /// <summary>
        /// Method used to change an atribute from a person.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personDto"></param>
        /// <returns>Changed person.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put(int id, [FromBody] PersonDTO personDto)
        {
            if (id != personDto.Id)
                throw new ApplicationException("The field id does not conform with requested in the body.");

            if (personDto == null)
                throw new ApplicationException($"Person with id {id} not found.");

            await this._personService.UpdateAsync(personDto);
            return Ok(personDto);
        }

        /// <summary>
        /// Method used to delete a person.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The deleted person.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            var personDto = await this._personService.GetPersonByIdAsync(id);

            if (personDto == null)
                throw new ApplicationException($"Person with id {id} not found.");

            await this._personService.RemoveAsync(id);
            return Ok(personDto);
        }
    }
}
