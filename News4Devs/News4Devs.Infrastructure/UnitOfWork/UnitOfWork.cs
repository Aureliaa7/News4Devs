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
        private IRepositoryBase<Article> articlesRepository;
        private IRepositoryBase<SavedArticle> savedArticlesRepository;

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

        public IRepositoryBase<Article> ArticlesRepository
        {
            get
            {
                if (articlesRepository == null)
                {
                    articlesRepository = new RepositoryBase<Article>(dbContext);
                }
                return articlesRepository;
            }
        }

        public IRepositoryBase<SavedArticle> SavedArticlesRepository
        {
            get
            {
                if (savedArticlesRepository == null)
                {
                    savedArticlesRepository = new RepositoryBase<SavedArticle>(dbContext);
                }
                return savedArticlesRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
