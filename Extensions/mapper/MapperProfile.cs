using AutoMapper;
using LoginProductMinimalApi.Entities;
using LoginProductMinimalApi.Models.Profile;

namespace LoginProductMinimalApi.Extensions.mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<User, User>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
