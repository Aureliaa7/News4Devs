using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News4Devs.Shared.DTOs;
using News4Devs.Shared.Entities;
using News4Devs.Shared.Enums;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Models;
using System;
using System.Threading.Tasks;

namespace News4Devs.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    public class ArticlesController : News4DevsController
    {
        private readonly IDevApiService devApiService;
        private readonly IArticleService articleService;
        private readonly IMapper mapper;

        public ArticlesController(
            IDevApiService devApiService,
            IArticleService articleService,
            IMapper mapper)
        {
            this.devApiService = devApiService;
            this.articleService = articleService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles([FromQuery] DevApiQueryParamsModel devApiQueryParamsModel)
        {
            var result = await devApiService.GetArticlesAsync(devApiQueryParamsModel);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("{userId}/save")]
        public async Task<IActionResult> MarkArticleAsSaved([FromRoute] Guid userId, ArticleDto articleDto)
        {
            var saveArticleModel = new SaveArticleModel
            {
                Article = mapper.Map<Article>(articleDto),
                ArticleSavingType = ArticleSavingType.Saved,
                UserId = userId
            };
            var savedArticle = await articleService.SaveArticleAsync(saveArticleModel);

            return Ok(mapper.Map<SavedArticleDto>(savedArticle));
        }

        [HttpGet]
        [Authorize]
        [Route("{userId}/saved")]
        public async Task<IActionResult> GetSavedArticles([FromRoute] Guid userId, [FromQuery] PaginationFilter filter)
        {
            var savedArticles = await articleService.GetSavedArticlesAsync(userId, filter);
            var savedArticlesDtos = mapper.Map<PagedResponseDto<ExtendedArticleDto>>(savedArticles);

            return Ok(savedArticlesDtos);
        }

        [HttpPost]
        [Authorize]
        [Route("{userId}/favorite")]
        public async Task<IActionResult> MarkArticleAsFavorite([FromRoute] Guid userId, ArticleDto articleDto)
        {
            var saveArticleModel = new SaveArticleModel
            {
                Article = mapper.Map<Article>(articleDto),
                ArticleSavingType = ArticleSavingType.Favorite,
                UserId = userId
            };
            var savedArticle = await articleService.SaveArticleAsFavoriteAsync(saveArticleModel);

            return Ok(mapper.Map<SavedArticleDto>(savedArticle));
        }

        [HttpGet]
        [Authorize]
        [Route("{userId}/favorite")]
        public async Task<IActionResult> GetFavoriteArticles([FromRoute] Guid userId, [FromQuery] PaginationFilter filter)
        {
            var favoriteArticles = await articleService.GetFavoriteArticlesAsync(userId, filter);
            var favoriteArticlesDtos = mapper.Map<PagedResponseDto<ExtendedArticleDto>>(favoriteArticles);

            return Ok(favoriteArticlesDtos);
        }

        [HttpDelete]
        [Authorize]
        [Route("{userId}/saved")]
        public async Task<IActionResult> RemoveFromSavedArticles([FromRoute] Guid userId, [FromBody] DeleteSavedArticleDto savedArticleDto)
        {
            await articleService.RemoveFromSavedArticlesAsync(userId, savedArticleDto.ArticleTitle);

            return NoContent();
        }

        [HttpDelete]
        [Authorize]
        [Route("{userId}/favorite")]
        public async Task<IActionResult> RemoveFromFavoriteArticles([FromRoute] Guid userId, [FromBody] DeleteSavedArticleDto savedArticleDto)
        {
            await articleService.RemoveFromFavoriteArticlesAsync(userId, savedArticleDto.ArticleTitle);

            return NoContent();
        }
    }
}
