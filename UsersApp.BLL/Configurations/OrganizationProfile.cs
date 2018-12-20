using AutoMapper;
using UsersApp.BLL.DTOs.Users;
using UsersApp.EF.Models;

namespace UsersApp.BLL.Configurations
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
