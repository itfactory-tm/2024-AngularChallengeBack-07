using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class SponsorProfile : Profile
    {
        public SponsorProfile()
        {
            // Map from Sponsor to SponsorDto
            CreateMap<Sponsor, SponsorDto>()
                /*.ForMember(dest => dest.Editions, opt => opt.MapFrom(src => src.Editions.Select(e => e.EditionName).ToList()))*/;

            // Map from SponsorDto to Sponsor
            CreateMap<SponsorDto, Sponsor>()
                .ForMember(dest => dest.Editions, opt => opt.Ignore()); // Ignore the Editie collection on reverse mapping
        }
    }
}
