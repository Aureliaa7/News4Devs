using News4Devs.Core.Entities;
using News4Devs.Core.Models;

namespace News4Devs.Core.Interfaces.Services
{
    public interface IArticlePaginationService : IPaginationService<ExtendedArticleModel, SavedArticle>
    {
    }
}
