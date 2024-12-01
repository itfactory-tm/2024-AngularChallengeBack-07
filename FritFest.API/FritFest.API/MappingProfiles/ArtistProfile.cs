using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.MappingProfiles
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<Artist, ArtistDto>()
                .ForMember(dest => dest.Editions, opt => opt.MapFrom(src => src.Edition.Select(e => e.EditionName).ToList()));
            //.ForMember(dest => dest.GenreNaam, opt => opt.MapFrom(src => src.Genre.Naam));
            CreateMap<ArtistDto, Artist>()
                .ForMember(dest => dest.Edition, opt => opt.Ignore());
                //.ForMember(dest => dest.Genre,opt=>opt.Ignore()).ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId));

        }
    }
}
