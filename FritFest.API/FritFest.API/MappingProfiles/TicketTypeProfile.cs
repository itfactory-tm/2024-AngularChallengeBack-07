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
            CreateMap<TicketType, TicketTypeDto>();

            // Map from TicketTypeDto to TicketType
            CreateMap<TicketTypeDto, TicketType>();
                 
        }
    }
}
