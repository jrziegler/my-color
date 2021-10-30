using MyColor.Application.DTOs;
using MyColor.Domain.Entities;
using MyColor.Domain.Utils;

namespace MyColor.Application.Mappings
{
    public static class DtoToDomainMapping
    {
        public static Person MapToDomain(this PersonDTO personDto)
        {
            return new Person(
                personDto.Id,
                personDto.Name,
                personDto.LastName,
                personDto.ZipCode,
                personDto.City,
                ApplicationColors.GetColorIdByName(personDto.Color)
            );
        }
    }
}
