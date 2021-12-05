using News4Devs.Core.Entities;
using News4Devs.Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace News4Devs.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<User> UsersRepository{ get; }

        IRepository<Article> ArticlesRepository { get; }

        IRepository<SavedArticle> SavedArticlesRepository { get; }

        Task SaveChangesAsync();
    }
}
