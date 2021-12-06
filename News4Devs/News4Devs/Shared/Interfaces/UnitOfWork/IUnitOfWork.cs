using News4Devs.Shared.Entities;
using News4Devs.Shared.Interfaces.Repositories;
using System.Threading.Tasks;

namespace News4Devs.Shared.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<User> UsersRepository{ get; }

        IRepository<Article> ArticlesRepository { get; }

        IRepository<SavedArticle> SavedArticlesRepository { get; }

        Task SaveChangesAsync();
    }
}
