using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.EditionName, opt => opt.MapFrom(src => src.Edition.EditionName))
                .ForMember(dest => dest.EditionId, opt => opt.MapFrom(src => src.Edition.EditionId));

            CreateMap<ArticleDto, Article>()
                .ForMember(dest => dest.Edition, opt => opt.Ignore()); // Editie object must be set explicitly
        }
    }
}
