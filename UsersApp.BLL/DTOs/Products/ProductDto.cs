using Newtonsoft.Json;

namespace UsersApp.BLL.DTOs.Products
{
    public class ProductDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
