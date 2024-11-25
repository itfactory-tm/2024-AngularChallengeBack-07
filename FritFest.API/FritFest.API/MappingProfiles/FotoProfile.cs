using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class FotoProfile : Profile
    {
        public FotoProfile()
        {
            // Map from Foto to FotoDto
            CreateMap<Foto, FotoDto>()
                .ForMember(dest => dest.EditieNaam, opt => opt.MapFrom(src => src.Editie.EditieNaam))
                .ForMember(dest => dest.ArtikelTitel, opt => opt.MapFrom(src => src.Artikel.Titel))
                .ForMember(dest => dest.PodiumNaam, opt => opt.MapFrom(src => src.Podium.Naam));

            // Reverse map from FotoDto to Foto
            CreateMap<FotoDto, Foto>()
                .ForMember(dest => dest.Editie, opt => opt.Ignore())   // Ignored to avoid overwriting navigation properties
                .ForMember(dest => dest.Artikel, opt => opt.Ignore())
                .ForMember(dest => dest.Podium, opt => opt.Ignore());
        }
    }
}
