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
                .ForMember(dest => dest.Edities, opt => opt.MapFrom(src => src.Editie));

            // Map from SponsorDto to Sponsor
            CreateMap<SponsorDto, Sponsor>()
                .ForMember(dest => dest.Editie, opt => opt.Ignore()); // Ignore the Editie collection on reverse mapping
        }
    }
}
