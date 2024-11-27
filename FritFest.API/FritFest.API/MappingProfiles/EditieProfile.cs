using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class EditieProfile : Profile
    {
        public EditieProfile()
        {
            // Main Editie mapping
            CreateMap<Editie, EditieDto>()
                .ForMember(dest => dest.TicketCount, opt => opt.MapFrom(src => src.Tickets != null ? src.Tickets.Count : 0))
                .ForMember(dest => dest.ArtiestNamen, opt => opt.MapFrom(src => src.Artiesten.Select(a => a.Naam).ToList()))
                .ForMember(dest => dest.Fotos, opt => opt.MapFrom(src => src.Fotos))
                .ForMember(dest => dest.ArtikelNamen, opt => opt.MapFrom(src => src.Artikelen.Select(a => a.Titel).ToList()))
                .ForMember(dest => dest.SponsorNamen, opt => opt.MapFrom(src => src.Sponsors.Select(s => s.SponsorNaam).ToList()))
                .ForMember(dest => dest.FoodtruckNamen, opt => opt.MapFrom(src => src.Foodtrucks.Select(f => f.Naam).ToList()));

            CreateMap<EditieDto, Editie>()
                .ForMember(dest => dest.Tickets, opt => opt.Ignore())
                .ForMember(dest => dest.Artiesten, opt => opt.Ignore())
                .ForMember(dest => dest.Fotos, opt => opt.Ignore())
                .ForMember(dest => dest.Sponsors, opt => opt.Ignore())
                .ForMember(dest => dest.Foodtrucks, opt => opt.Ignore())
                .ForMember(dest => dest.Artikelen, opt => opt.Ignore())
                ;
        }
    }
}
