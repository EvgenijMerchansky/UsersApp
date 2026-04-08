using System.Collections.Generic;

namespace Users.Example.DBLayer.Models;

public class Product
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual IEnumerable<User> Users { get; set; }
}
