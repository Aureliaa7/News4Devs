using News4Devs.Core.Entities;
using News4Devs.Core.Enums;
using News4Devs.Core.Exceptions;
using News4Devs.Core.Interfaces.Services;
using News4Devs.Core.Interfaces.UnitOfWork;
using News4Devs.Core.Models;
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

        public async Task<SavedArticle> SaveArticleAsync(SaveArticleModel saveArticleModel)
        {
            await CheckEntitiesExistenceAsync(saveArticleModel);

            bool articleExists = await unitOfWork.ArticlesRepository.ExistsAsync(x => x.Title == saveArticleModel.Article.Title);
            if (!articleExists)
            {
                await unitOfWork.ArticlesRepository.AddAsync(saveArticleModel.Article);
                await unitOfWork.SaveChangesAsync();
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
            bool userExists = await unitOfWork.UsersRepository.ExistsAsync(x => x.Id == saveArticleModel.UserId);
            if (!userExists)
            {
                throw new EntityNotFoundException($"The user with the id {saveArticleModel.UserId} does not exist!");
            }

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
    }
}
