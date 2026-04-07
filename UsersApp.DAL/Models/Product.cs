using Newtonsoft.Json;
using System.Collections.Generic;

namespace UsersApp.DAL.Models
{
    public class Product
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<User> Users { get; set; }
    }
}
