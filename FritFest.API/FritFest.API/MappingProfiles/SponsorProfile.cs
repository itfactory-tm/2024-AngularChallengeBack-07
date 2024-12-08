using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class SponsorProfile : Profile
    {
        public SponsorProfile()
        {
            // Map from Sponsors to SponsorDto
            CreateMap<Sponsor, SponsorDto>()
                .ForMember(dest => dest.EditionId, opt => opt.MapFrom(src => src.Edition.EditionId))
                .ForMember(dest => dest.EditionName, opt => opt.MapFrom(src => src.Edition.EditionName))
               ;

            // Map from SponsorDto to Sponsors
            CreateMap<SponsorDto, Sponsor>()
                .ForMember(dest => dest.Edition, opt => opt.Ignore()) // Ignore the Editie collection on reverse mapping
                ;
        }
    }
}
