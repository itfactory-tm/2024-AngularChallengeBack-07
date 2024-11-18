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
            // Map from Genre to GenreDto
            CreateMap<Genre, GenreDto>()
                .ForMember(dest => dest.ArtiestNamen, opt => opt.MapFrom(src => src.Artiesten.Select(a => a.Naam).ToList()));

            // Reverse map from GenreDto to Genre
            CreateMap<GenreDto, Genre>()
                .ForMember(dest => dest.Artiesten, opt => opt.Ignore()); // Ignore Artiesten to avoid overwriting navigation property
        }
    }
}
