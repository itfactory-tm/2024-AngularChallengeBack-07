using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;
using System.Linq;

namespace FritFest.API.Profiles
{
    public class LocatieProfile : Profile
    {
        public LocatieProfile()
        {
            // Map from Locatie to LocatieDto
            CreateMap<Locatie, LocatieDto>()
                .ForMember(dest => dest.FoodTruckNamen, opt => opt.MapFrom(src => src.FoodTrucks.Select(ft => ft.Naam).ToList()))
                .ForMember(dest => dest.PodiumNamen, opt => opt.MapFrom(src => src.Podia.Select(p => p.Naam).ToList()));

            // Map from LocatieDto to Locatie, excluding navigation properties to avoid overwriting
            CreateMap<LocatieDto, Locatie>()
                .ForMember(dest => dest.FoodTrucks, opt => opt.Ignore())
                .ForMember(dest => dest.Podia, opt => opt.Ignore());
        }
    }
}
