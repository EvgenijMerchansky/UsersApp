using AzureFromTheTrenches.Commanding.Abstractions;
using System.Collections.Generic;
using UsersApp.BLL.DTOs.Products;
using UsersApp.BLL.DTOs.Users;

namespace UsersApp.WebApi.AzureFunctions.Commands
{
    public class GetUserCommand : ICommand<UserDto>
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<ProductDto> Products { get; private set; }
    }
}
