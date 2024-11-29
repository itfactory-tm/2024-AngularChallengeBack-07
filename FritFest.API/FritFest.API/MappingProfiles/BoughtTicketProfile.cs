using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class BoughtTicketProfile : Profile
    {
        public BoughtTicketProfile()
        {
            CreateMap<BoughtTicket, BoughtTicketDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Ticket.TicketType.Name)); // Map Editie name

            CreateMap<BoughtTicketDto, BoughtTicket>()
                .ForMember(dest => dest.Ticket, opt => opt.Ignore());
        }
    }
}
