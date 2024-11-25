using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class TicketTypeProfile : Profile
    {
        public TicketTypeProfile()
        {
            // Map from TicketType to TicketTypeDto
            CreateMap<TicketType, TicketTypeDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Ticket.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Ticket.LastName))
                .ForMember(dest => dest.TelNr, opt => opt.MapFrom(src => src.Ticket.TelNr))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Ticket.Email))
                .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.Ticket.TicketId));

            // Map from TicketTypeDto to TicketType
            CreateMap<TicketTypeDto, TicketType>()
                .ForMember(dest => dest.Ticket, opt => opt.Ignore()); // Ignore Ticket navigation property in reverse mapping
        }
    }
}
