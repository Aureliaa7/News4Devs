using News4Devs.Core.Entities;
using News4Devs.Core.Models;
using News4Devs.Core.Pagination;
using System;
using System.Threading.Tasks;

namespace News4Devs.Core.Interfaces.Services
{
    public interface IArticleService
    {
        Task<SavedArticle> SaveArticleAsync(SaveArticleModel saveArticleModel);

        Task<SavedArticle> SaveArticleAsFavoriteAsync(SaveArticleModel saveArticleModel);

        Task<PagedResponseModel<ExtendedArticleModel>> GetSavedArticlesAsync(Guid userId, PaginationFilter paginationFilter);

        Task<PagedResponseModel<ExtendedArticleModel>> GetFavoriteArticlesAsync(Guid userId, PaginationFilter paginationFilter);

        Task<string> RemoveFromSavedArticlesAsync(Guid userId, string articleTitle);

        Task<string> RemoveFromFavoriteArticlesAsync(Guid userId, string articleTitle);
    }
}
