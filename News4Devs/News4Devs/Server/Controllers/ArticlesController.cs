using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News4Devs.Shared.DTOs;
using News4Devs.Shared.Entities;
using News4Devs.Shared.Enums;
using News4Devs.Shared.Interfaces.Services;
using News4Devs.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace News4Devs.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    public class ArticlesController : News4DevsController
    {
        private readonly INewsApiService newsApiService;
        private readonly IArticleService articleService;
        private readonly IMapper mapper;

        public ArticlesController(
            INewsApiService newsApiService,
            IArticleService articleService,
            IMapper mapper)
        {
            this.newsApiService = newsApiService;
            this.articleService = articleService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles([FromQuery] NewsApiQueryParamsModel queryParamsModel)
        {
            // used only for testing purposes in order to avoid making too many requests to News API
            //await Task.Delay(0);
            //StreamReader sr = new StreamReader(@"C:\Users\Aura.LAPTOP-GLQOS0K8\Desktop\facultate\Master\Anul1\Sem2\ModelareaSiEvaluareaPerformantelor\proiect\News4Devs\News4Devs\News4Devs\Server\newsDataForTesting.json");
            //var jsonData = sr.ReadToEnd();
            //var testData = JsonConvert.DeserializeObject<NewsApiResponseDto>(jsonData);
            //var extendedArticles = new List<ExtendedArticleDto>();  
            //testData.articles.ToList()
            //        .ForEach(x => extendedArticles.Add(new ExtendedArticleDto { Article = x }));

            //var res = new NewsApiResponseModel
            //{
            //    Status = testData.status,
            //    TotalResults = testData.totalResults,
            //    Articles = extendedArticles
            //};

            //return Ok(res);

            var result = await newsApiService.GetArticlesAsync(queryParamsModel);
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
