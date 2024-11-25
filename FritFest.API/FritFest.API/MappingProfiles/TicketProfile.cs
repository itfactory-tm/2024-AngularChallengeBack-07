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
                .ForMember(dest => dest.EditieNaam, opt => opt.MapFrom(src => src.Editie.EditieNaam)) // Map Editie name
                .ForMember(dest => dest.TicketPrijs, opt => opt.MapFrom(src => src.TicketType.Prijs))
                .ForMember(dest => dest.DagNaam, opt => opt.MapFrom(src => src.Dag.Naam)); // Map Dag name

            // Map from TicketDto to Ticket
            CreateMap<TicketDto, Ticket>()
                .ForMember(dest => dest.Editie, opt => opt.Ignore()) // Ignore navigation properties
                .ForMember(dest => dest.TicketType, opt => opt.Ignore())
                .ForMember(dest => dest.Dag, opt => opt.Ignore());
        }
    }
}
