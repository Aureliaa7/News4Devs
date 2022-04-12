using AutoMapper;
using News4Devs.Shared.DTOs;
using News4Devs.Shared.Entities;
using News4Devs.Shared.Models;

namespace News4Devs.Infrastructure.MappingConfigurations
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateUserMappings();
            CreateArticlesMappings();
            CreateMessagesMappings();
        }

        private void CreateUserMappings()
        {
            CreateMap<RegisterDto, User>().ForMember(x => x.ProfilePhotoName, opt => opt.Ignore());
            CreateMap<User, UserDto>();
            CreateMap<PagedResponseModel<User>, PagedResponseDto<UserDto>>();
        }

        private void CreateArticlesMappings()
        {
            CreateMap<ArticleDto, Article>().ReverseMap();

            CreateMap<SavedArticle, SavedArticleDto>();

            CreateMap<ExtendedArticleModel, ExtendedArticleDto>();
            CreateMap<PagedResponseModel<ExtendedArticleModel>, PagedResponseDto<ExtendedArticleDto>>();
        }

        private void CreateMessagesMappings()
        {
            CreateMap<ChatMessage, ChatMessageDto>().ReverseMap();
            CreateMap<PagedResponseModel<ChatMessage>, PagedResponseDto<ChatMessageDto>>();
        }
    }
}
