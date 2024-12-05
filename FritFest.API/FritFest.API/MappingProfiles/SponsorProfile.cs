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
                .ForMember(dest => dest.EditionName, opt => opt.MapFrom(src => src.Edition.EditionName))
                .ForMember(dest => dest.SponsorLogoBase64, opt => opt.MapFrom(src => src.SponsorLogo != null
                ? Convert.ToBase64String(src.SponsorLogo):null));

            // Map from SponsorDto to Sponsors
            CreateMap<SponsorDto, Sponsor>()
                .ForMember(dest => dest.Edition, opt => opt.Ignore()) // Ignore the Editie collection on reverse mapping
                .ForMember(dest => dest.SponsorLogo, opt => opt.Ignore());
        }
    }
}
