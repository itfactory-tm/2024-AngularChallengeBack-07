using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class TimeSlotProfile : Profile
    {
        public TimeSlotProfile()
        {
            // Map from TijdStip to TijdStipDto
            CreateMap<TimeSlot, TimeSlotDto>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name)) // Map Artiest Naam
                .ForMember(dest => dest.StageName, opt => opt.MapFrom(src => src.Stage.Name)); // Map Podium Naam

            // Map from TijdStipDto to TijdStip (if you need reverse mapping)
            CreateMap<TimeSlotDto, TimeSlot>()
                .ForMember(dest => dest.Artist, opt => opt.Ignore()) // Ignore Artiest navigation property during reverse mapping
                .ForMember(dest => dest.Stage, opt => opt.Ignore()); // Ignore Podium navigation property during reverse mapping
        }
    }
}
