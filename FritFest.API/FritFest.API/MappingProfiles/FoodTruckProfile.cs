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
            // Map from FoodTruck to FoodTruckDto
            CreateMap<FoodTruck, FoodTruckDto>()
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : null))
                .ForMember(dest => dest.Editions, opt => opt.MapFrom(src => src.Editions.Select(e => e.EditionName).ToList()))
                .ForMember(dest => dest.MenuItemCount, opt => opt.MapFrom(src => src.MenuItems != null ? src.MenuItems.Count : 0));

            // Reverse map from FoodTruckDto to FoodTruck
            CreateMap<FoodTruckDto, FoodTruck>()
                .ForMember(dest => dest.Location, opt => opt.Ignore()) // Locatie will be managed separately
                .ForMember(dest => dest.Editions, opt => opt.Ignore()) // Related collections are ignored
                .ForMember(dest => dest.MenuItems, opt => opt.Ignore());
        }
    }
}
