using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UsersApp.BLL.Services;
using UsersApp.EF.Context;
using UsersApp.EF.Models;

namespace UsersApp.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        // should be refactored (moved to generic (base) repository).
        private readonly UsersContext _context;

        public UserRepository(UsersContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(int id, CancellationToken token = default(CancellationToken))
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id, token);

            return user;
        }

        public async Task CreateUserAsync(User user, CancellationToken token = default(CancellationToken))
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync(token);
        }

        public async Task UpdateUserAsync(User updatedUser, CancellationToken token = default(CancellationToken))
        {
            User exUser = await GetUserAsync(updatedUser.Id);

            exUser.FirstName = updatedUser.FirstName;

            exUser.LastName = updatedUser.LastName;

            _context.Update(exUser);

            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteUserAsync(User user, CancellationToken token = default(CancellationToken))
        {
            _context.Remove(user);

            await _context.SaveChangesAsync(token);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}