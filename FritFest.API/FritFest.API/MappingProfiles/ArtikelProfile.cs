using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class ArtikelProfile : Profile
    {
        public ArtikelProfile()
        {
            CreateMap<Artikel, ArtikelDto>()
                .ForMember(dest => dest.EditieNaam, opt => opt.MapFrom(src => src.Editie.EditieNaam))
                .ForMember(dest => dest.EditieId, opt => opt.MapFrom(src => src.Editie.EditieId));

            CreateMap<ArtikelDto, Artikel>()
                .ForMember(dest => dest.Editie, opt => opt.Ignore()); // Editie object must be set explicitly
        }
    }
}
