﻿namespace News4Devs.Core
{
    public static class Constants
    {
        public const string MigrationsAssembly = "News4Devs.Infrastructure";
        public const string APIAssembly = "Server";
        public const string ClientAssembly = "Client";

        public const string UserId = "UserId";
        
        public const string ImageExtension = ".png";
        public const string ProfilePhotosDirectory = "wwwroot\\profile-photos";

        public const int NotFound = 404;
        public const int Conflict = 409;
        public const int BadRequest = 400;

        public const int DefaultPageSize = 10;
        public const int DefaultPageNumber = 1;

        public const string ArticlesAddress = "https://localhost:44347/api/v1/articles/";
        public const string SavedArticlesEndpoint = "saved";
        public const string FavoriteArticlesEndpoint = "favorite";
    }
}
