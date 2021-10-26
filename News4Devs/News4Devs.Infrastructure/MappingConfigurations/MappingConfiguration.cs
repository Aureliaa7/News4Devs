using AutoMapper;
using News4Devs.Core.DTOs;
using News4Devs.Core.Entities;
using News4Devs.Core.Models;

namespace News4Devs.Infrastructure.MappingConfigurations
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMappings();
        }

        private void CreateMappings()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<User, UserDto>();

            CreateMappingsForNewsCatcherAPI();
        }

        private void CreateMappingsForNewsCatcherAPI()
        {
            //CreateMap<NewsCatcherApiResponse, NewsCatcherApiResponseDto>()
            //    .ForMember(dest => dest.Page_Size, opt => opt.MapFrom(src => src.PageSize))
            //    .ForMember(dest => dest.Total_Pages, opt => opt.MapFrom(src => src.TotalPages));

            //CreateMap<Article, ArticleDto>()
            //    .ForMember(dest => dest._Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Published_Date, opt => opt.MapFrom(src => src.PublishedDate));
        }
    }
}
