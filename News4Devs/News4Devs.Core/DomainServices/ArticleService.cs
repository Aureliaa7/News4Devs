using News4Devs.Core.Entities;
using News4Devs.Core.Enums;
using News4Devs.Core.Exceptions;
using News4Devs.Core.Interfaces.Services;
using News4Devs.Core.Interfaces.UnitOfWork;
using News4Devs.Core.Models;
using News4Devs.Core.Pagination;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace News4Devs.Core.DomainServices
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork unitOfWork;

        public ArticleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<PagedResponseModel<ExtendedArticleModel>> GetSavedArticlesAsync(Guid userId, PaginationFilter paginationFilter)
        {
            return await GetArticlesByUserAndSavingType(ArticleSavingType.Saved, userId, paginationFilter);
        }

        private async Task<PagedResponseModel<ExtendedArticleModel>> GetArticlesByUserAndSavingType(
            ArticleSavingType savingType, 
            Guid userId, 
            PaginationFilter paginationFilter)
        {
            await CheckIfUserExistsAsync(userId);

            Expression<Func<SavedArticle, bool>> filter = x => x.UserId == userId &&
                (x.ArticleSavingType == savingType || x.ArticleSavingType == ArticleSavingType.SavedAndFavorite);

            return await GetPagedResponseModelAsync(savingType, paginationFilter, filter);
        }

        private async Task<PagedResponseModel<ExtendedArticleModel>> GetPagedResponseModelAsync(
            ArticleSavingType savingType,
            PaginationFilter paginationFilter,
            Expression<Func<SavedArticle, bool>> filter)
        {
            string apiEndpoint = savingType == ArticleSavingType.Saved ?
                Constants.SavedArticlesEndpoint :
                Constants.FavoriteArticlesEndpoint;

            int totalRecords = await unitOfWork.SavedArticlesRepository.GetTotalRecordsAsync(filter);

            var totalPages = ((double)totalRecords / (double)paginationFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            var savedArticles = (await unitOfWork.SavedArticlesRepository.GetAllAsync(filter,
                includeProperties: nameof(Article),
                skip: (paginationFilter.PageNumber - 1) * paginationFilter.PageSize,
                take: paginationFilter.PageSize))
                .Select(x => new ExtendedArticleModel
                {
                    Article = x.Article,
                    IsSaved = x.ArticleSavingType == ArticleSavingType.Saved || x.ArticleSavingType == ArticleSavingType.SavedAndFavorite,
                    IsFavorite = x.ArticleSavingType == ArticleSavingType.Favorite || x.ArticleSavingType == ArticleSavingType.SavedAndFavorite,
                })
                .ToList();
            
            return new PagedResponseModel<ExtendedArticleModel>
            {
                Data = savedArticles,
                TotalPages = roundedTotalPages,
                PageNumber = paginationFilter.PageNumber,
                PageSize = paginationFilter.PageSize,
                PreviousPage = GetPreviousPage(savingType, paginationFilter.PageNumber, paginationFilter.PageSize),
                NextPage = GetNextPage(savingType, paginationFilter.PageNumber, paginationFilter.PageSize, roundedTotalPages),
                FirstPage = GetPageAddress(apiEndpoint, 1, paginationFilter.PageSize),
                LastPage = GetPageAddress(apiEndpoint, roundedTotalPages, paginationFilter.PageSize),
                TotalRecords = totalRecords
            };
        }

        private string GetPreviousPage(ArticleSavingType savingType, int pageNumber, int pageSize)
        {
            string endpoint = savingType == ArticleSavingType.Saved ?
                Constants.SavedArticlesEndpoint : Constants.FavoriteArticlesEndpoint;

            return pageNumber > 1 ? GetPageAddress(endpoint, pageNumber-1, pageSize) : null;
        }

        private string GetNextPage(ArticleSavingType savingType, int pageNumber, int pageSize, int totalPages)
        {
            bool isLastPage = pageNumber == totalPages;
            if (isLastPage)
            {
                return null;
            }

            if (savingType == ArticleSavingType.Saved)
            {
                return GetPageAddress(Constants.SavedArticlesEndpoint, ++pageNumber, pageSize);
            }
            return GetPageAddress(Constants.FavoriteArticlesEndpoint, ++pageNumber, pageSize);
        }

        private string GetPageAddress(string endpoint, int pageNumber, int pageSize)
        {
            return $"{Constants.ArticlesAddress}{endpoint}?pageNumber={pageNumber}&pageSize={pageSize}";
        }

        public async Task<SavedArticle> SaveArticleAsync(SaveArticleModel saveArticleModel)
        {
            return await SaveArticle(saveArticleModel,
                newArticleSavingType: ArticleSavingType.Saved,
                existingArticleSavingType: ArticleSavingType.Favorite);
        }

        private async Task CheckEntitiesExistenceAsync(SaveArticleModel saveArticleModel)
        {
            await CheckIfUserExistsAsync(saveArticleModel.UserId);

            bool articleWasSaved = await unitOfWork.SavedArticlesRepository.ExistsAsync(
                x => x.Article.Title == saveArticleModel.Article.Title &&
                x.UserId == saveArticleModel.UserId &&
                (x.ArticleSavingType == saveArticleModel.ArticleSavingType ||
                x.ArticleSavingType == ArticleSavingType.SavedAndFavorite));

            if (articleWasSaved)
            {
                throw new DuplicateEntityException($"The article named {saveArticleModel.Article.Title} was already saved " +
                    $"by the user with the id {saveArticleModel.UserId}");
            }
        }

        private async Task CheckIfUserExistsAsync(Guid id)
        {
            bool userExists = await unitOfWork.UsersRepository.ExistsAsync(x => x.Id == id);
            if (!userExists)
            {
                throw new EntityNotFoundException($"The user with the id {id} does not exist!");
            }
        }

        public async Task<SavedArticle> SaveArticleAsFavoriteAsync(SaveArticleModel saveArticleModel)
        {
            return await SaveArticle(saveArticleModel,
                newArticleSavingType: ArticleSavingType.Favorite,
                existingArticleSavingType: ArticleSavingType.Saved);
        }

        private async Task<SavedArticle> SaveArticle(
            SaveArticleModel saveArticleModel,
            ArticleSavingType newArticleSavingType,
            ArticleSavingType existingArticleSavingType)
        {
            await CheckEntitiesExistenceAsync(saveArticleModel);

            bool articleExists = await unitOfWork.ArticlesRepository.ExistsAsync(x => x.Title == saveArticleModel.Article.Title);
            if (!articleExists)
            {
                await unitOfWork.ArticlesRepository.AddAsync(saveArticleModel.Article);
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                SavedArticle existingArticle = await unitOfWork.SavedArticlesRepository.GetFirstOrDefaultAsync(
                    x => x.ArticleTitle == saveArticleModel.Article.Title &&
                    x.UserId == saveArticleModel.UserId &&
                    (x.ArticleSavingType == existingArticleSavingType ||
                    x.ArticleSavingType == ArticleSavingType.SavedAndFavorite));

                if (existingArticle != null)
                {
                    existingArticle.ArticleSavingType = ArticleSavingType.SavedAndFavorite;
                    var updatedSavedArticle = await unitOfWork.SavedArticlesRepository.UpdateAsync(existingArticle);
                    await unitOfWork.SaveChangesAsync();

                    return updatedSavedArticle;
                }
            }

            var savedArticle = new SavedArticle
            {
                ArticleSavingType = newArticleSavingType,
                ArticleTitle = saveArticleModel.Article.Title,
                UserId = saveArticleModel.UserId
            };
            await unitOfWork.SavedArticlesRepository.AddAsync(savedArticle);
            await unitOfWork.SaveChangesAsync();

            return savedArticle;
        }

        public async Task<PagedResponseModel<ExtendedArticleModel>> GetFavoriteArticlesAsync(
            Guid userId, 
            PaginationFilter paginationFilter)
        {
            return await GetArticlesByUserAndSavingType(ArticleSavingType.Favorite, userId, paginationFilter);
        }

        public async Task<string> RemoveFromSavedArticlesAsync(Guid userId, string articleTitle)
        {
            return await RemoveArticleFromSpecificListAsync(
                userId, 
                articleTitle, 
                oldSavingType: ArticleSavingType.Saved, 
                newSavingTypeIfNeeded: ArticleSavingType.Favorite);
        }

        private async Task DeleteArticleIfNotUsedByOtherUsersAsync(Guid userIdToBeExcluded, string articleTitle)
        {
            bool articleIsSavedByOtherUsers = await unitOfWork.SavedArticlesRepository.ExistsAsync(
                x => x.ArticleTitle == articleTitle && x.UserId != userIdToBeExcluded);
            if (!articleIsSavedByOtherUsers)
            {
                var articleToBeDeleted = await unitOfWork.ArticlesRepository.GetFirstOrDefaultAsync(x => x.Title == articleTitle);
                await unitOfWork.ArticlesRepository.RemoveAsync(articleToBeDeleted);
            }
        }

        public async Task<string> RemoveFromFavoriteArticlesAsync(Guid userId, string articleTitle)
        {
            return await RemoveArticleFromSpecificListAsync(
                userId, 
                articleTitle, 
                oldSavingType: ArticleSavingType.Favorite, 
                newSavingTypeIfNeeded: ArticleSavingType.Saved);
        }

        private async Task<string> RemoveArticleFromSpecificListAsync(
            Guid userId, string articleTitle, 
            ArticleSavingType oldSavingType, 
            ArticleSavingType newSavingTypeIfNeeded)
        {
            await CheckIfUserExistsAsync(userId);
            var savedArticle = await unitOfWork.SavedArticlesRepository.GetFirstOrDefaultAsync(
                x => x.ArticleTitle == articleTitle && x.UserId == userId &&
                (x.ArticleSavingType == oldSavingType || x.ArticleSavingType == ArticleSavingType.SavedAndFavorite));
            if (savedArticle != null && savedArticle.ArticleSavingType == oldSavingType)
            {
                await unitOfWork.SavedArticlesRepository.RemoveAsync(savedArticle.Id);
                await DeleteArticleIfNotUsedByOtherUsersAsync(userId, articleTitle);
            }
            else if (savedArticle != null && savedArticle.ArticleSavingType == ArticleSavingType.SavedAndFavorite)
            {
                savedArticle.ArticleSavingType = newSavingTypeIfNeeded;
                await unitOfWork.SavedArticlesRepository.UpdateAsync(savedArticle);
            }

            await unitOfWork.SaveChangesAsync();

            return articleTitle;
        }
    }
}
