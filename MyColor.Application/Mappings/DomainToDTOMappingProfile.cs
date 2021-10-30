using AutoMapper;
using MyColor.Application.DTOs;
using MyColor.Domain.Entities;
using MyColor.Domain.Utils;

namespace MyColor.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Person, PersonDTO>()
                .ForMember(pDto => pDto.Id, p => p.MapFrom(source => source.Id))
                .ForMember(pDto => pDto.City, p => p.MapFrom(source => source.City))
                .ForMember(pDto => pDto.LastName, p => p.MapFrom(source => source.LastName))
                .ForMember(pDto => pDto.Name, p => p.MapFrom(source => source.Name))
                .ForMember(pDto => pDto.ZipCode, p => p.MapFrom(source => source.ZipCode))
                .ForMember(pDto => pDto.Color, p => p.MapFrom(source => ApplicationColors.GetColorNameById(source.ColorId)));
        }
    }
}
