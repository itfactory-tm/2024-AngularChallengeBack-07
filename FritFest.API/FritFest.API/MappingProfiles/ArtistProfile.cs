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
                .ForMember(dest => dest.EditionName, opt => opt.MapFrom(src => src.Edition.EditionName))
                .ForMember(dest => dest.EditionId, opt => opt.MapFrom(src => src.Edition.EditionId));


            CreateMap<ArtistDto, Artist>()
                .ForMember(dest => dest.Edition, opt => opt.Ignore());


        }
    }
}
