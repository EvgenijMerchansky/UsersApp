namespace UsersApp.EF.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using UsersApp.DAL;
    using UsersApp.EF.Context;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly UsersContext _usersContext;

        public UnitOfWork(
            UsersContext context,
            IUserRepository userRepository)
        {
            _usersContext = context;
            UserRepository = userRepository;
        }

        public IUserRepository UserRepository { get; }

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
