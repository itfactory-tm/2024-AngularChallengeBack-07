using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;
using System.Linq;

namespace FritFest.API.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            // Map from Locatie to LocatieDto
            CreateMap<Location, LocationDto>()
                .ForMember(dest => dest.FoodTruckNames, opt => opt.MapFrom(src => src.FoodTrucks.Select(ft => ft.Name).ToList()))
                .ForMember(dest => dest.StageNames, opt => opt.MapFrom(src => src.Stages.Select(p => p.Name).ToList()));

            // Map from LocatieDto to Locatie, excluding navigation properties to avoid overwriting
            CreateMap<LocationDto, Location>()
                .ForMember(dest => dest.FoodTrucks, opt => opt.Ignore())
                .ForMember(dest => dest.Stages, opt => opt.Ignore());
        }
    }
}
