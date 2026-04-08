using AutoMapper;
using Users.Example.DBLayer.Models;
using Users.Example.Models.Dtos.Products;
using Users.Example.Models.Dtos.Users;

namespace Users.Example.Utilities.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<ProductDto, Product>().ReverseMap();
    }
}