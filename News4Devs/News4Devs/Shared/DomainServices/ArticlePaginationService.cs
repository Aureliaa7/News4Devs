using News4Devs.Shared.Entities;
using News4Devs.Shared.Enums;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Interfaces.UnitOfWork;
using News4Devs.Shared.Models;
using News4Devs.Shared.Pagination;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace News4Devs.Shared.DomainServices
{
    public class ArticlePaginationService : PaginationServiceBase<ExtendedArticleModel, SavedArticle>, IArticlePaginationService
    {
        private readonly IUnitOfWork unitOfWork;

        public ArticlePaginationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public override async Task<PagedResponseModel<ExtendedArticleModel>> GetPagedResponseAsync(
            string address,
            PaginationFilter paginationFilter,
            Expression<Func<SavedArticle, bool>> filter = null)
        {
            int totalRecords = await unitOfWork.SavedArticlesRepository.GetTotalRecordsAsync(filter);

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

            var pagedResponse = GetPagedResponseModel(savedArticles, address, totalRecords, paginationFilter);
            return pagedResponse;
        }
    }
}
