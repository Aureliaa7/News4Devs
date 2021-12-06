using AutoMapper;
using News4Devs.Shared.DTOs;
using News4Devs.Shared.Entities;
using News4Devs.Shared.Models;
using News4Devs.Shared.Pagination;

namespace News4Devs.Infrastructure.MappingConfigurations
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateUserMappings();
            CreateArticlesMappings();

            CreateMap<PagedResponseModel<ExtendedArticleModel>, PagedResponseDto<ExtendedArticleDto>>();
        }

        private void CreateUserMappings()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<User, UserDto>();
        }

        private void CreateArticlesMappings()
        {
            CreateMap<ArticleDto, Article>()
               .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.published_at))
               .ForMember(dest => dest.ReadablePublishDate, opt => opt.MapFrom(src => src.readable_publish_date))
               .ForMember(dest => dest.ReadingTimeMinutes, opt => opt.MapFrom(src => src.reading_time_minutes))
               .ForMember(dest => dest.SocialImageUrl, opt => opt.MapFrom(src => src.social_image))
               .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.user.name))
               .ForMember(dest => dest.AuthorWebsiteUrl, opt => opt.MapFrom(src => src.user.website_url))
               .ReverseMap();

            CreateMap<SavedArticle, SavedArticleDto>()
                .ForPath(dest => dest.Article.user.name, opt => opt.MapFrom(src => src.Article.AuthorName))
                .ForPath(dest => dest.Article.user.website_url, opt => opt.MapFrom(src => src.Article.AuthorWebsiteUrl));

            CreateMap<ExtendedArticleModel, ExtendedArticleDto>();
        }
    }
}
