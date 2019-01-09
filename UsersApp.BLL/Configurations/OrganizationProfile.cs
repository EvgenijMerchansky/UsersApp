using AutoMapper;
using UsersApp.BLL.DTOs.Products;
using UsersApp.BLL.DTOs.Users;
using UsersApp.DAL.Models;

namespace UsersApp.BLL.Configurations
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<int, GetUserDto>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<DeleteUserDto, User>();
            CreateMap<User, User>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}