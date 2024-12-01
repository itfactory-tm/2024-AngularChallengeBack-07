using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;
using System.Linq;

namespace FritFest.API.Profiles
{
    public class DayProfile : Profile
    {
        public DayProfile()
        {
            CreateMap<Day, DayDto>()
                .ForMember(dest => dest.TicketCount, opt => opt.MapFrom(src => src.Tickets != null ? src.Tickets.Count : 0));

            CreateMap<DayDto, Day>()
                .ForMember(dest => dest.Tickets, opt => opt.Ignore()); // Tickets should be managed outside the DTO
        }
    }
}
