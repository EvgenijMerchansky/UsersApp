using System.Threading;
using System.Threading.Tasks;
using UsersApp.BLL.Services;
using UsersApp.EF.Context;
using UsersApp.EF.Interfaces;

namespace UsersApp.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; }

        private readonly UsersContext _usersContext;

        public UnitOfWork(
            IUserRepository userRepository, 
            UsersContext context)
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
