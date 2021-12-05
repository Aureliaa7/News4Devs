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
        private IRepository<User> usersRepository;
        private IRepository<Article> articlesRepository;
        private IRepository<SavedArticle> savedArticlesRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IRepository<User> UsersRepository
        {
            get
            {
                if (usersRepository == null)
                {
                    usersRepository = new Repository<User>(dbContext);
                }
                return usersRepository;
            }
        }

        public IRepository<Article> ArticlesRepository
        {
            get
            {
                if (articlesRepository == null)
                {
                    articlesRepository = new Repository<Article>(dbContext);
                }
                return articlesRepository;
            }
        }

        public IRepository<SavedArticle> SavedArticlesRepository
        {
            get
            {
                if (savedArticlesRepository == null)
                {
                    savedArticlesRepository = new Repository<SavedArticle>(dbContext);
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
