using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UsersApp.EF.Context;
using UsersApp.DAL.Models;
using UsersApp.DAL;

namespace UsersApp.EF.Repositories
{
    public class UserRepository : BaseRepository<int, User, UsersContext>, IUserRepository
    {
        // should be refactored (moved to generic (base) repository).
        //private readonly UsersContext _context;

        public UserRepository(UsersContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken token = default(CancellationToken))
        {
            IEnumerable<User> users = await DbContext.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetUserAsync(int id, CancellationToken token = default(CancellationToken))
        {
            User user = await GetAsync(id, token);

            return user;
        }

        public async Task CreateUserAsync(User user, CancellationToken token = default(CancellationToken))
        {
            await DbContext.Users.AddAsync(user);

            await DbContext.SaveChangesAsync(token);
        }

        public async Task UpdateUserAsync(int id, User updatedUser, CancellationToken token = default(CancellationToken))
        {
            User exUser = await GetUserAsync(id);

            exUser.FirstName = updatedUser.FirstName;

            exUser.LastName = updatedUser.LastName;

            DbContext.Update(exUser);

            await DbContext.SaveChangesAsync(token);
        }

        public async Task DeleteUserAsync(User user, CancellationToken token = default(CancellationToken))
        {
            User deleteUser = await GetUserAsync(user.Id);

            DbContext.Users.Remove(deleteUser);

            await DbContext.SaveChangesAsync(token);
        }

        public async Task<bool> UserExistsAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await DbContext.Users.AnyAsync(p => p.Id == id, ct);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}