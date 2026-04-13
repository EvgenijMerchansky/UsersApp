using AutoMapper;
using Users.Example.DBLayer.Models;
using Users.Example.Models.Dtos.Users;

namespace Users.Example.Utilities.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<UserDto, User>().ReverseMap();
    }
}