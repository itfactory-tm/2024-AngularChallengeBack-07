using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            // Map from MenuItem to MenuItemDto
            CreateMap<MenuItem, MenuItemDto>()
                .ForMember(dest => dest.FoodTruckNaam, opt => opt.MapFrom(src => src.FoodTruck.Naam));

            // Map from MenuItemDto to MenuItem, ignoring navigation property
            CreateMap<MenuItemDto, MenuItem>()
                .ForMember(dest => dest.FoodTruck, opt => opt.Ignore());
        }
    }
}
