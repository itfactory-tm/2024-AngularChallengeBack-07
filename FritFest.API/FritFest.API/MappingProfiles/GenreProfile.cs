using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;
using System.Linq;

namespace FritFest.API.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            // Map from Genres to GenreDto
            CreateMap<Genre, GenreDto>();
                /*.ForMember(dest => dest.ArtistsNames, opt => opt.MapFrom(src => src.Artists.Select(a => a.Name).ToList()))*/;

            // Reverse map from GenreDto to Genres
            CreateMap<GenreDto, Genre>();
                /*.ForMember(dest => dest.Artists, opt => opt.Ignore());*/ // Ignore Artiesten to avoid overwriting navigation property
        }
    }
}
