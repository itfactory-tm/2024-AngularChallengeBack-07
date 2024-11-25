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
                .ForMember(dest => dest.LocatieNaam, opt => opt.MapFrom(src => src.Locatie.Naam))
                .ForMember(dest => dest.TijdStippen, opt => opt.MapFrom(src => src.TijdStippen.Select(ts => ts.Tijd).ToList()))
                .ForMember(dest => dest.Fotos, opt => opt.MapFrom(src => src.Fotos.Select(f => f.Bestand).ToList()))
                ;

            // Map from PodiumDto to Podium
            CreateMap<PodiumDto, Podium>()
                .ForMember(dest => dest.Locatie, opt => opt.Ignore()) // Ignore Locatie on reverse mapping to avoid circular reference issues
                .ForMember(dest => dest.TijdStippen, opt => opt.Ignore()) // Optionally ignore TijdStip if not directly needed for creation
                .ForMember(dest => dest.Fotos, opt => opt.Ignore()); // Optionally ignore Fotos if not directly needed for creation
        }
    }
}
