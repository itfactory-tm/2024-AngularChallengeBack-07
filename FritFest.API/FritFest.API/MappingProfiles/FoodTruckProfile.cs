using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;
using System.Linq;

namespace FritFest.API.Profiles
{
    public class FoodTruckProfile : Profile
    {
        public FoodTruckProfile()
        {
            CreateMap<FoodTruck, FoodTruckDto>()
                .ForMember(dest => dest.LocationName,
                    opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : null))
                .ForMember(dest => dest.EditionName, opt => opt.MapFrom(src => src.Edition.EditionName))
                .ForMember(dest => dest.MenuItems,
                    opt => opt.MapFrom(src => src.MenuItems.Select(mi => mi.Name).ToList()));
            // Reverse map from FoodTruckDto to FoodTrucks
            CreateMap<FoodTruckDto, FoodTruck>()
                .ForMember(dest => dest.Location, opt => opt.Ignore()) // Locatie will be managed separately
                .ForMember(dest => dest.Edition, opt => opt.Ignore()) // Related collections are ignored
                .ForMember(dest => dest.MenuItems, opt => opt.Ignore());
        }
    }
}
