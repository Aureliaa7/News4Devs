using News4Devs.Core.Entities;
using News4Devs.Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace News4Devs.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepositoryBase<User> UsersRepository{ get; }

        Task SaveChangesAsync();
    }
}
