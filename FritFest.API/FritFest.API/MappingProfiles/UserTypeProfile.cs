using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.MappingProfiles
{

    public class UserTypeProfile : Profile
    {
        public UserTypeProfile()
        {
            CreateMap<UserType, UserTypeDto>()
                .ForMember(dest => dest.Users,
                    opt => opt.MapFrom(src => src.Users.Select(u => u.Name).ToList()));

            CreateMap<UserTypeDto, UserType>()
                .ForMember(dest => dest.Users, opt => opt.Ignore());
        }
    }
}