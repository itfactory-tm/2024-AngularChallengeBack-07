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
                .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.Tickets))
                .ForMember(dest => dest.Artiesten, opt => opt.MapFrom(src => src.Artiesten))
                .ForMember(dest => dest.Fotos, opt => opt.MapFrom(src => src.Fotos))
                .ForMember(dest => dest.Artikelen, opt => opt.MapFrom(src => src.Artikelen))
                .ForMember(dest => dest.Sponsors, opt => opt.MapFrom(src => src.Sponsors))
                .ForMember(dest => dest.Foodtrucks, opt => opt.MapFrom(src => src.Foodtrucks));

            CreateMap<EditieDto, Editie>();

            // Related entity mappings
            CreateMap<Ticket, TicketDto>();
            CreateMap<TicketDto, Ticket>();

            CreateMap<Artiest, ArtiestDto>()
                .ForMember(dest => dest.GenreNaam, opt => opt.MapFrom(src => src.Genre.Naam));
            CreateMap<ArtiestDto, Artiest>();

            CreateMap<Foto, FotoDto>();
            CreateMap<FotoDto, Foto>();

            CreateMap<Artikel, ArtikelDto>();
            CreateMap<ArtikelDto, Artikel>();

            CreateMap<Sponsor, SponsorDto>();
            CreateMap<SponsorDto, Sponsor>();

            CreateMap<FoodTruck, FoodTruckDto>();
            CreateMap<FoodTruckDto, FoodTruck>();
        }
    }
}
