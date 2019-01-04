using Newtonsoft.Json;

namespace UsersApp.BLL.DTOs.Users
{
    public class UpdateUserDto
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
