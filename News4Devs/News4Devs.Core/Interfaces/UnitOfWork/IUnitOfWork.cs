using News4Devs.Core.Entities;
using News4Devs.Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace News4Devs.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepositoryBase<User> UsersRepository{ get; }

        IRepositoryBase<Article> ArticlesRepository { get; }

        IRepositoryBase<SavedArticle> SavedArticlesRepository { get; }

        Task SaveChangesAsync();
    }
}
