using AutoMapper;
using FritFest.API.Dtos;
using UserType = FritFest.API.Entities.UserType;

namespace FritFest.API.MappingProfiles;

public class UserTypeProfile : Profile
{
    public UserTypeProfile()
    {
        CreateMap<UserType, UserTypeDto>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.Select(u => u.Naam).ToList()));

        CreateMap<UserTypeDto, UserType>()
            .ForMember(dest => dest.Users, opt => opt.Ignore());
    }
}