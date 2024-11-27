using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class GekochteTicketProfile : Profile
    {
        public GekochteTicketProfile()
        {
            CreateMap<GekochteTicket, GekochteTicketDto>()
                .ForMember(dest => dest.Naam, opt => opt.MapFrom(src => src.Ticket.TicketType.Naam)); // Map Editie name

            CreateMap<GekochteTicketDto, GekochteTicket>()
                .ForMember(dest => dest.Ticket, opt => opt.Ignore());
        }
    }
}
