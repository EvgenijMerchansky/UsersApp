using System;
using Newtonsoft.Json;

namespace UsersApp.EF.Models
{
    public class Product
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
