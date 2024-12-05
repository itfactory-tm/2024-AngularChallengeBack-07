using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            // Map from MenuItems to MenuItemDto
            CreateMap<MenuItem, MenuItemDto>()
                .ForMember(dest => dest.FoodTruckId, opt => opt.MapFrom(src => src.FoodTruck.FoodTruckId))
                .ForMember(dest => dest.FoodTruckName, opt => opt.MapFrom(src => src.FoodTruck.Name));

            // Map from MenuItemDto to MenuItems, ignoring navigation property
            CreateMap<MenuItemDto, MenuItem>()
                .ForMember(dest => dest.FoodTruck, opt => opt.Ignore());
        }
    }
}
