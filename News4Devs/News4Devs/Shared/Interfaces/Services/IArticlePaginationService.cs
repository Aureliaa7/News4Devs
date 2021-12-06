using News4Devs.Shared.Entities;
using News4Devs.Shared.Models;

namespace News4Devs.Shared.Interfaces.Services
{
    public interface IArticlePaginationService : IPaginationService<ExtendedArticleModel, SavedArticle>
    {
    }
}
