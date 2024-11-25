using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.TypeNaam, opt => opt.MapFrom(src => src.Type.Naam));

        CreateMap<UserDto, User>().ForMember(dest => dest.Type,opt=>opt.Ignore());
    }
}