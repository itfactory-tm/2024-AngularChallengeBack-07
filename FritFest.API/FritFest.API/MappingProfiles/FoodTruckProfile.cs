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
                .ForMember(dest => dest.LocatieNaam, opt => opt.MapFrom(src => src.Locatie != null ? src.Locatie.Naam : null))
                
                .ForMember(dest => dest.MenuItemCount, opt => opt.MapFrom(src => src.MenuItems != null ? src.MenuItems.Count : 0));

            // Reverse map from FoodTruckDto to FoodTruck
            CreateMap<FoodTruckDto, FoodTruck>()
                .ForMember(dest => dest.Locatie, opt => opt.Ignore()) // Locatie will be managed separately
                .ForMember(dest => dest.Edities, opt => opt.Ignore()) // Related collections are ignored
                .ForMember(dest => dest.MenuItems, opt => opt.Ignore());
        }
    }
}
