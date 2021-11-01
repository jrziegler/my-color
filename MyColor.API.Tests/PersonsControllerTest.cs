using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyColor.API.Controllers;
using MyColor.Application.DTOs;
using MyColor.Application.Interfaces;
using MyColor.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyColor.API.Tests
{
    public class PersonsControllerTest
    {
        private readonly PersonsController _sut;
        private readonly Mock<IPersonService> _personServiceMock = new Mock<IPersonService>();
        private readonly Mock<ILoggerService> _loggerMock = new Mock<ILoggerService>();

        private IEnumerable<PersonDTO> _personsDto;

        public PersonsControllerTest()
        {
            _sut = new PersonsController(_personServiceMock.Object, _loggerMock.Object);
            _personsDto = GetPersons();
        }

        [Fact]
        public async Task Get_ShouldReturnAListOfPersonsDto_WhenPersonsExist()
        {
            // Arrange
            _personServiceMock.Setup(m => m.GetPersonsAsync()).ReturnsAsync(_personsDto);

            // Act
            var result = await _sut.Get();
            var okResult = result.Result as OkObjectResult;
            var persons = okResult.Value;

            // Assert
            persons.Should().Be(_personsDto);
        }

        [Fact]
        public async Task Get_ShouldReturnAPersonDto_WhenPersonExists()
        {
            // Arrange
            _personServiceMock.Setup(m => m.GetPersonByIdAsync(1)).ReturnsAsync(_personsDto.FirstOrDefault(person => person.Id.Equals(1)));
            var expected = await _personServiceMock.Object.GetPersonByIdAsync(1);

            // Act
            var result = await _sut.Get(1);
            var okResult = result.Result as OkObjectResult;
            var person = okResult.Value;

            // Assert
            person.Should().Be(expected);
        }

        [Fact]
        public async Task Get_ShouldReturnAPersonDto_WhenPersonWithColorExists()
        {
            // Arrange
            _personServiceMock.Setup(m => m.GetPersonByColorAsync("blau")).ReturnsAsync(_personsDto.Where(person => person.Color.Equals("blau")));
            var expected = await _personServiceMock.Object.GetPersonByColorAsync("blau");

            // Act
            var result = await _sut.Get("blau");
            var okResult = result.Result as OkObjectResult;
            var person = okResult.Value;

            // Assert
            person.Should().Be(expected);
        }

        [Fact]
        public async Task Post_ShouldReturnTheCreatedPersonDto_WhenPersonDataOk()
        {
            // Arrange
            var expected = new PersonDTO
            {
                Id = 1,
                Name = "Müller",
                LastName = "Hans",
                ZipCode = "67742",
                City = "Lauterecken",
                Color = "blau"
            };
            _personServiceMock.Setup(m => m.AddAsync(expected)).ReturnsAsync(expected);

            // Act
            var result = await _sut.Post(expected);
            var okResult = result as CreatedAtRouteResult;
            var person = okResult.Value;

            // Assert
            person.Should().Be(expected);
        }

        //TODO: make the tests for:
        //      * Get with false value color
        //      * Post: a) without name or lastname
        //              b) without object at http body
        //              c) false color
        //      * Put:  a) without name or lastname
        //              b) without object at http body
        //              c) false color
        //              d) false id
        //      * Del:  a) false id
        //              b) true id
        
        /*
        [Fact]
        public async Task Put_ShouldReturnTheModifiedPersonDto_WhenPersonDataOk()
        {
            // Arrange
            var expected = await _personServiceMock.Object.GetPersonByIdAsync(1);
            expected.Name = "Fred";

            _personServiceMock.Setup(m => m.UpdateAsync(expected));

            // Act
            var result = await _sut.Put(1, expected);
            var okResult = result as OkObjectResult;
            //var person = okResult.Value;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }
        */

        private IEnumerable<PersonDTO> GetPersons()
        {
            return new List<PersonDTO>
            {
                new PersonDTO
                {
                    Id = 1,
                    Name = "Müller",
                    LastName = "Hans",
                    ZipCode = "67742",
                    City = "Lauterecken",
                    Color = "blau"
                },
                new PersonDTO
                {
                    Id = 2,
                    Name = "Petersen",
                    LastName = "Peter",
                    ZipCode = "18439",
                    City = "Stralsund",
                    Color = "grün"
                },
                new PersonDTO
                {
                    Id = 3,
                    Name = "Johnson",
                    LastName = "Johnny",
                    ZipCode = "88888",
                    City = "made up",
                    Color = "violett"
                },
                new PersonDTO
                {
                    Id = 4,
                    Name = "Millenium",
                    LastName = "Milly",
                    ZipCode = "77777",
                    City = "made up too",
                    Color = "rot"
                },
                new PersonDTO
                {
                    Id = 5,
                    Name = "Müller",
                    LastName = "Jonas",
                    ZipCode = "32323",
                    City = "Hansstadt",
                    Color = "gelb"
                },
                new PersonDTO
                {
                    Id = 6,
                    Name = "Fujitsu",
                    LastName = "Tastatur",
                    ZipCode = "42342",
                    City = "Japan",
                    Color = "türkis"
                },
                new PersonDTO
                {
                    Id = 7,
                    Name = "Andersson",
                    LastName = "Anders",
                    ZipCode = "32132",
                    City = "Schweden - ☀",
                    Color = "grün"
                },
                new PersonDTO
                {
                    Id = 8,
                    Name = "Bart",
                    LastName = "Bartman",
                    ZipCode = "12313",
                    City = "Wasweißich",
                    Color = "blau"
                },
                new PersonDTO
                {
                    Id = 9,
                    Name = "Gerber",
                    LastName = "Gerda",
                    ZipCode = "76535",
                    City = "Woanders",
                    Color = "violett"
                },
                new PersonDTO
                {
                    Id = 10,
                    Name = "Klaussen",
                    LastName = "Klaus",
                    ZipCode = "43246",
                    City = "Hierach",
                    Color = "grün"
                }
            };
        }
    }
}
