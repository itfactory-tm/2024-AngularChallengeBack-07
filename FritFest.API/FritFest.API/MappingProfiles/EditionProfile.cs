using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class EditionProfile : Profile
    {
        public EditionProfile()
        {
            // Main Editie mapping
            CreateMap<Edition, EditionDto>()
                .ForMember(dest => dest.TicketCount, opt => opt.MapFrom(src => src.Tickets != null ? src.Tickets.Count : 0))
                //.ForMember(dest => dest.ArtistsNames, opt => opt.MapFrom(src => src.Artists.Select(a => a.Name).ToList()))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Select(p => p.File).ToList()))
                .ForMember(dest => dest.ArticleNames, opt => opt.MapFrom(src => src.Articles.Select(a => a.Title).ToList()))
                .ForMember(dest => dest.SponsorNames, opt => opt.MapFrom(src => src.Sponsors.Select(s => s.SponsorName).ToList()))
                .ForMember(dest => dest.FoodtruckNames, opt => opt.MapFrom(src => src.Foodtrucks.Select(f => f.Name).ToList()));

            CreateMap<EditionDto, Edition>()
                .ForMember(dest => dest.Tickets, opt => opt.Ignore())
                //.ForMember(dest => dest.Artists, opt => opt.Ignore())
                .ForMember(dest => dest.Photos, opt => opt.Ignore())
                .ForMember(dest => dest.Sponsors, opt => opt.Ignore())
                .ForMember(dest => dest.Foodtrucks, opt => opt.Ignore())
                .ForMember(dest => dest.Articles, opt => opt.Ignore())
                ;
        }
    }
}
