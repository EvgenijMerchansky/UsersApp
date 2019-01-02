using System.Collections.Generic;
using Newtonsoft.Json;

namespace UsersApp.DAL.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
