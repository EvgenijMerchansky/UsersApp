using Microsoft.EntityFrameworkCore;
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
        public UserRepository(UsersContext context) : base(context)
        {
        }

        public async override Task Update(int id, User entity)
        {
            User exUser = await GetAsync(id);

            exUser.FirstName = entity.FirstName;

            exUser.LastName = entity.LastName;

            await base.Update(id, exUser);
        }

        public async Task<bool> UserExistsAsync(
            int id, 
            CancellationToken ct = default(CancellationToken))
        {
            return await DbContext.Users.AnyAsync(p => p.Id == id, ct);
        }
    }
}