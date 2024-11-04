using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.MappingProfiles
{
    public class ArtiestProfile : Profile
    {
        public ArtiestProfile()
        {
            CreateMap<Artiest, ArtiestDto>();
        }
    }
}
