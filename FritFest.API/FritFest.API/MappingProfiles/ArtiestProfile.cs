using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.MappingProfiles
{
    public class ArtiestProfile : Profile
    {
        public ArtiestProfile()
        {
            CreateMap<Artiest, ArtiestDto>().ForMember(dest => dest.GenreNaam, opt => opt.MapFrom(src => src.Genre.Naam));
            CreateMap<ArtiestDto, Artiest>().ForMember(dest => dest.Genre,opt=>opt.Ignore()).ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId));
        }
    }
}
