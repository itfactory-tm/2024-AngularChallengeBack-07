using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class StageProfile : Profile
    {
        public StageProfile()
        {
            CreateMap<Stage, StageDto>()
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name))

                .ForMember(dest => dest.TimeSlotRanges, opt => opt.MapFrom(src => 

                    src.TimeSlots.Select(ts => $"{ts.StartTime:HH:mm} - {ts.EndTime:HH:mm}").ToList()))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Select(f => f.File).ToList()))
                ;

            // Map from PodiumDto to Podium
            CreateMap<StageDto, Stage>()
                .ForMember(dest => dest.Location, opt => opt.Ignore()) // Ignore Locatie on reverse mapping to avoid circular reference issues
                .ForMember(dest => dest.TimeSlots, opt => opt.Ignore()) // Optionally ignore TijdStip if not directly needed for creation
                .ForMember(dest => dest.Photos, opt => opt.Ignore()); // Optionally ignore Fotos if not directly needed for creation
        }
    }
}
