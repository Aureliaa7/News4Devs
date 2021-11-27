using News4Devs.Core.Entities;
using News4Devs.Core.Enums;
using News4Devs.Core.Exceptions;
using News4Devs.Core.Interfaces.Services;
using News4Devs.Core.Interfaces.UnitOfWork;
using News4Devs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IList<ExtendedArticleModel>> GetSavedArticlesAsync(Guid userId)
        {
            await CheckIfUserExistsAsync(userId);

            var savedArticles = await GetArticlesByUserAndSavingType(ArticleSavingType.Saved, userId);

            return savedArticles;
        }

        private async Task<IList<ExtendedArticleModel>> GetArticlesByUserAndSavingType(ArticleSavingType savingType, Guid userId)
        {
            var savedArticles = (await unitOfWork.SavedArticlesRepository.GetAllAsync(x => x.UserId == userId &&
                (x.ArticleSavingType == savingType || x.ArticleSavingType == ArticleSavingType.SavedAndFavorite), 
                includeProperties: nameof(Article)))
                .Select(x => new ExtendedArticleModel
                    {
                        Article = x.Article,
                        IsSaved = x.ArticleSavingType == ArticleSavingType.Saved || x.ArticleSavingType == ArticleSavingType.SavedAndFavorite,
                        IsFavorite = x.ArticleSavingType == ArticleSavingType.Favorite || x.ArticleSavingType == ArticleSavingType.SavedAndFavorite,
                    })
                .ToList();

            return savedArticles;
        }

        public async Task<SavedArticle> SaveArticleAsync(SaveArticleModel saveArticleModel)
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
                SavedArticle favoriteArticle = await unitOfWork.SavedArticlesRepository.GetFirstOrDefaultAsync(
                    x => x.ArticleTitle == saveArticleModel.Article.Title && x.UserId == saveArticleModel.UserId
                    && x.ArticleSavingType == ArticleSavingType.Favorite);

                if (favoriteArticle != null)
                {
                    favoriteArticle.ArticleSavingType = ArticleSavingType.SavedAndFavorite;
                    var updatedSavedArticle = await unitOfWork.SavedArticlesRepository.UpdateAsync(favoriteArticle);
                    await unitOfWork.SaveChangesAsync();

                    return updatedSavedArticle;
                }
            }

            var savedArticle = new SavedArticle
            {
                ArticleSavingType = ArticleSavingType.Saved,
                ArticleTitle = saveArticleModel.Article.Title,
                UserId = saveArticleModel.UserId
            };
            await unitOfWork.SavedArticlesRepository.AddAsync(savedArticle);
            await unitOfWork.SaveChangesAsync();

            return savedArticle;
        }

        private async Task CheckEntitiesExistenceAsync(SaveArticleModel saveArticleModel)
        {
            await CheckIfUserExistsAsync(saveArticleModel.UserId);

            bool articleWasSaved = await unitOfWork.SavedArticlesRepository.ExistsAsync(
                x => x.Article.Title == saveArticleModel.Article.Title &&
                x.UserId == saveArticleModel.UserId &&
                x.ArticleSavingType == saveArticleModel.ArticleSavingType);

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
    }
}
