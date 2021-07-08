using System.Collections.Generic;
using UsersApp.BLL.DTOs.Products;

namespace UsersApp.BLL.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}
