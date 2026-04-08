using System.Collections.Generic;

namespace Users.Example.DBLayer.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual IEnumerable<Product> Products { get; set; }
}