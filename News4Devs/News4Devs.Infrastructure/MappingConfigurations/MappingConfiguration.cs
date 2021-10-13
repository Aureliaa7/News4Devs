using AutoMapper;
using News4Devs.Core.DTOs;
using News4Devs.Core.Entities;

namespace News4Devs.Infrastructure.MappingConfigurations
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<RegisterDto, User>();

            CreateMap<User, UserDto>();
        }
    }
}
