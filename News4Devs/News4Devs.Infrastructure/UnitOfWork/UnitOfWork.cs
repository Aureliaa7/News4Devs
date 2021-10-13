using News4Devs.Core.Entities;
using News4Devs.Core.Interfaces.Repositories;
using News4Devs.Core.Interfaces.UnitOfWork;
using News4Devs.Infrastructure.AppDbContext;
using News4Devs.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace News4Devs.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        private IRepositoryBase<User> usersRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IRepositoryBase<User> UsersRepository
        {
            get
            {
                if (usersRepository == null)
                {
                    usersRepository = new RepositoryBase<User>(dbContext);
                }
                return usersRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
