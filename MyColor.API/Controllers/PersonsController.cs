using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyColor.Application.DTOs;
using MyColor.Application.Interfaces;
using MyColor.Infra.Logging.Interfaces;

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
        public async Task<ActionResult<IEnumerable<PersonDTO>>> Get()
        {
            try
            {
                _logger.LogInfo("Trying to get persons.");
                var persons = await this._personService.GetPersonsAsync();
                if (persons == null)
                {
                    return NotFound("Persons not found.");
                }

                return Ok(persons);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Method used to get a person by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A person with his favorite color.</returns>
        [HttpGet("{id:int}", Name = "GetPerson")]
        public async Task<ActionResult<PersonDTO>> Get(int id)
        {
            try
            {
                var person = await this._personService.GetPersonByIdAsync(id);
                if (person == null)
                {
                    return NotFound("Person not found.");
                }

                return Ok(person);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Method used to get a list of persons by color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns>A list of persons, which the favorite color is equal the parameter.</returns>
        [HttpGet("color/{color}")]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> Get(string color)
        {
            try
            {
                var persons = await this._personService.GetPersonByColorAsync(color);
                if (!persons.Any())
                {
                    return NotFound("Persons not found.");
                }

                return Ok(persons);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Method used to add a new person to the system.
        /// </summary>
        /// <param name="personDto"></param>
        /// <returns>The person created.</returns>
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

        /// <summary>
        /// Method used to change an atribute from a person.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personDto"></param>
        /// <returns>Changed person.</returns>
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

        /// <summary>
        /// Method used to delete a person.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The deleted person.</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var personDto = await this._personService.GetPersonByIdAsync(id);

            if (personDto == null)
            {
                string msg = "Person not found.";
                //this._logger.LogWarn(msg);
                return NotFound(msg);
            }

            try
            {
                await this._personService.RemoveAsync(id);
                return Ok(personDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
