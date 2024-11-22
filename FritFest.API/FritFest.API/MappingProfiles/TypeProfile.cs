using AutoMapper;
using FritFest.API.Dtos;
using Type = FritFest.API.Entities.Type;

namespace FritFest.API.MappingProfiles;

public class TypeProfile : Profile
{
    public TypeProfile()
    {
        CreateMap<Type, TypeDto>()
            .ForMember(dest => dest.Users,
                opt => opt.MapFrom(src => src.Users.Select(u => u.Name).ToList()));

        CreateMap<TypeDto, Type>()
            .ForMember(dest => dest.Users, opt => opt.Ignore());
    }
}