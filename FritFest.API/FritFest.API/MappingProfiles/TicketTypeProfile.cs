using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class TicketTypeProfile : Profile
    {
        public TicketTypeProfile()
        {
            // Map from TicketTypes to TicketTypeDto
            CreateMap<TicketType, TicketTypeDto>();

            // Map from TicketTypeDto to TicketTypes
            CreateMap<TicketTypeDto, TicketType>();
                 
        }
    }
}
