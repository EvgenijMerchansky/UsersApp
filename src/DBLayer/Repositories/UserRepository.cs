namespace Users.Example.DBLayer.Repositories;

using Users.Example.DBLayer.EntityFramework;
using Users.Example.DBLayer.Models;
using Users.Example.DBLayer.Repositories.Interfaces;

public class UserRepository(UsersContext context) : BaseRepository<int, User, UsersContext>(context), IUserRepository { }