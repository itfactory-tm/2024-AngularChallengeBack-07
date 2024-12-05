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
                
                .ForMember(dest => dest.TicketTypeName, opt => opt.MapFrom(src => src.Ticket.TicketType.Name))
                .ForMember(dest => dest.EditionName, opt => opt.MapFrom(src => src.Ticket.Edition.EditionName));

            CreateMap<BoughtTicketDto, BoughtTicket>()
                .ForMember(dest => dest.Ticket, opt => opt.Ignore());
        }
    }
}
