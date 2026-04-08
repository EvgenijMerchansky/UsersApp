using Users.Example.DBLayer.Models;

namespace Users.Example.DBLayer.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<int, User> { }