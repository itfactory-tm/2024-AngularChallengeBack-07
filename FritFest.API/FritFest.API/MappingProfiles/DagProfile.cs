using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;
using System.Linq;

namespace FritFest.API.Profiles
{
    public class DagProfile : Profile
    {
        public DagProfile()
        {
            CreateMap<Dag, DagDto>()
                .ForMember(dest => dest.TicketCount, opt => opt.MapFrom(src => src.Tickets != null ? src.Tickets.Count : 0));

            CreateMap<DagDto, Dag>()
                .ForMember(dest => dest.Tickets, opt => opt.Ignore()); // Tickets should be managed outside the DTO
        }
    }
}
