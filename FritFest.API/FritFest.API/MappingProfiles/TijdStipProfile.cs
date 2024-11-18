using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class TijdStipProfile : Profile
    {
        public TijdStipProfile()
        {
            // Map from TijdStip to TijdStipDto
            CreateMap<TijdStip, TijdStipDto>()
                .ForMember(dest => dest.ArtiestNaam, opt => opt.MapFrom(src => src.Artiest.Naam)) // Map Artiest Naam
                .ForMember(dest => dest.PodiumNaam, opt => opt.MapFrom(src => src.Podium.Naam)); // Map Podium Naam

            // Map from TijdStipDto to TijdStip (if you need reverse mapping)
            CreateMap<TijdStipDto, TijdStip>()
                .ForMember(dest => dest.Artiest, opt => opt.Ignore()) // Ignore Artiest navigation property during reverse mapping
                .ForMember(dest => dest.Podium, opt => opt.Ignore()); // Ignore Podium navigation property during reverse mapping
        }
    }
}
