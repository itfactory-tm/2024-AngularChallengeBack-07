using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.MappingProfiles
{
    public class EditieProfile : Profile
    {
        public EditieProfile()
        {
            CreateMap<EditieDto, Editie>();
            CreateMap<Editie,EditieDto>();
        }
    }
}
