using News4Devs.Core.Entities;
using News4Devs.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Core.Interfaces.Services
{
    public interface IArticleService
    {
        Task<SavedArticle> SaveArticleAsync(SaveArticleModel saveArticleModel);

        Task<IList<ExtendedArticleModel>> GetSavedArticlesAsync(Guid userId);
    }
}
