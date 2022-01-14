namespace News4Devs.Shared
{
    public static class Constants
    {
        public const string MigrationsAssembly = "News4Devs.Infrastructure";
        public const string APIAssembly = "Server";
        public const string ClientAssembly = "Client";

        public const string UserId = "UserId";
        public const string UserFullName = "UserFullName";
        
        public const string ImageExtension = ".png";
        public const string ProfilePhotosDirectory = "wwwroot\\profile-photos";

        public const int NotFound = 404;
        public const int Conflict = 409;
        public const int BadRequest = 400;

        public const int DefaultPageSize = 20;
        public const int DefaultPageNumber = 1;

        public const string ArticlesAddress = "https://localhost:44395/api/v1/articles/";
        public const string SavedArticlesEndpoint = "saved";
        public const string FavoriteArticlesEndpoint = "favorite";

        public const string ChatAddress = "https://localhost:44395/api/v1/chat/";
        public const string AccountsAddress = "https://localhost:44395/api/v1/accounts";

        public const string BaseUrl = "https://localhost:44395/api/v1/";
    }
}
