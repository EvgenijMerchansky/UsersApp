using System;
using Newtonsoft.Json;

namespace UsersApp.EF.Models
{
    public class User
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
