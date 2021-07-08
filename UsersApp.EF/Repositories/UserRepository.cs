namespace UsersApp.DAL.EF.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UsersApp.DAL;
    using UsersApp.DAL.EF.Context;
    using UsersApp.DAL.Models;

    public class UserRepository : BaseRepository<int, User, UsersContext>, IUserRepository
    {
        // should be refactored (moved to generic (base) repository).
        public UserRepository(
            UsersContext context)
            : base(context)
        {
        }

        public async Task<bool> UserExistsAsync(
            int id,
            CancellationToken ct = default(CancellationToken))
        {
            return await DbContext.Users.AnyAsync(p => p.Id == id, ct);
        }
    }
}