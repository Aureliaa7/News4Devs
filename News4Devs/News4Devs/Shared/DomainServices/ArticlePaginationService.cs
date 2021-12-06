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
            int roundedTotalPages = GetRoundedTotalPages(totalRecords, paginationFilter.PageSize);

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
                PreviousPage = GetPreviousPage(address, paginationFilter.PageNumber, paginationFilter.PageSize, roundedTotalPages),
                NextPage = GetNextPage(address, paginationFilter.PageNumber, paginationFilter.PageSize, roundedTotalPages),
                FirstPage = GetPageAddress(address, 1, paginationFilter.PageSize, roundedTotalPages),
                LastPage = GetPageAddress(address, roundedTotalPages, paginationFilter.PageSize, roundedTotalPages),
                TotalRecords = totalRecords
            };
        }
    }
}
