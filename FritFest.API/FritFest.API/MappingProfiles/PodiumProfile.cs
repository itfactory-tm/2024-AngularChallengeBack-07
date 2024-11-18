using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class PodiumProfile : Profile
    {
        public PodiumProfile()
        {
            // Map from Podium to PodiumDto
            CreateMap<Podium, PodiumDto>()
                .ForMember(dest => dest.LocatieNaam, opt => opt.MapFrom(src => src.Locatie.Naam));

            // Map from PodiumDto to Podium
            CreateMap<PodiumDto, Podium>()
                .ForMember(dest => dest.Locatie, opt => opt.Ignore()) // Ignore Locatie on reverse mapping to avoid circular reference issues
                .ForMember(dest => dest.TijdStippen, opt => opt.Ignore()) // Optionally ignore TijdStippen if not directly needed for creation
                .ForMember(dest => dest.Fotos, opt => opt.Ignore()); // Optionally ignore Fotos if not directly needed for creation
        }
    }
}
