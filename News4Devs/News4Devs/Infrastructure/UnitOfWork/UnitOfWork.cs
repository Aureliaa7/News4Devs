using News4Devs.Shared.Entities;
using News4Devs.Shared.Interfaces.Repositories;
using News4Devs.Shared.Interfaces.UnitOfWork;
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
        private IRepository<ChatMessage> messagesRepository;

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

        public IRepository<ChatMessage> MessagesRepository
        {
            get
            {
                if (messagesRepository == null)
                {
                    messagesRepository = new Repository<ChatMessage>(dbContext);
                }
                return messagesRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
