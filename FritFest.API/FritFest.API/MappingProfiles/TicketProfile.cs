using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            // Map from Ticket to TicketDto
            CreateMap<Ticket, TicketDto>()
                .ForMember(dest => dest.EditionName, opt => opt.MapFrom(src => src.Edition.EditionName)) // Map Editie name
                .ForMember(dest => dest.TicketPrice, opt => opt.MapFrom(src => src.TicketType.Price))
                .ForMember(dest => dest.DayName, opt => opt.MapFrom(src => src.Day.Name)); // Map Dag name

            // Map from TicketDto to Ticket
            CreateMap<TicketDto, Ticket>()
                .ForMember(dest => dest.Edition, opt => opt.Ignore()) // Ignore navigation properties
                .ForMember(dest => dest.TicketType, opt => opt.Ignore())
                .ForMember(dest => dest.Day, opt => opt.Ignore());
        }
    }
}
