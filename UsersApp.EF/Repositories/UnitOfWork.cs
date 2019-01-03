using System.Threading;
using System.Threading.Tasks;
using UsersApp.EF.Context;
using UsersApp.DAL;

namespace UsersApp.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; }

        private readonly UsersContext _usersContext;

        public UnitOfWork(
            UsersContext context,
            IUserRepository userRepository)
        {
            _usersContext = context;
            UserRepository = userRepository;
        }

        public void Dispose()
        {
            _usersContext.Dispose();
        }

        public async Task CommitAsync(CancellationToken token)
        {
            await _usersContext.SaveChangesAsync(token);
        }
    }
}
