using AutoMapper;
using System.Collections.Generic;
using UsersApp.BLL.DTOs.Users;
using UsersApp.DAL.Models;

namespace UsersApp.BLL.Configurations
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<User, User>().ReverseMap();
            CreateMap<int, GetUserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<DeleteUserDto, User>();
            CreateMap<IEnumerable<UserDto>, IEnumerable<User>>().ReverseMap();
        }
    }
}
