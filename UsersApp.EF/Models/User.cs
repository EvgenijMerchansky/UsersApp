﻿using System;
using System.Collections.Generic;
using MessagePack.Formatters;
using Newtonsoft.Json;

namespace UsersApp.EF.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Product> Products { get; set; }
    }
}
