using AutoMapper;
using FritFest.API.Dtos;
using FritFest.API.Entities;

namespace FritFest.API.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            // Map from Foto to FotoDto
            CreateMap<Photo, PhotoDto>()
                .ForMember(dest => dest.EditionId, opt => opt.MapFrom(src => src.Edition.EditionId))
                .ForMember(dest => dest.EditionName, opt => opt.MapFrom(src => src.Edition.EditionName))
                .ForMember(dest => dest.ArticleId, opt => opt.MapFrom(src => src.Article.ArticleId))
                .ForMember(dest => dest.ArticleTitle, opt => opt.MapFrom(src => src.Article.Title))
                ;

            // Reverse map from FotoDto to Foto
            CreateMap<PhotoDto, Photo>()
                .ForMember(dest => dest.Edition, opt => opt.Ignore())   // Ignored to avoid overwriting navigation properties
                .ForMember(dest => dest.Article, opt => opt.Ignore())
                ;
        }
    }
}
