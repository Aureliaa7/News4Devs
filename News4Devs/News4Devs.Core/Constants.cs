namespace News4Devs.Core
{
    public static class Constants
    {
        public const string MigrationsAssembly = "News4Devs.Infrastructure";
        public const string UserId = "UserId";
        public const string ProfileImagesPath = 
            @"C:\Users\Aura.LAPTOP-GLQOS0K8\Desktop\facultate\Master\Anul1\Sem1\IngineriaAplicatiilorWeb\proiect\News4Devs\News4Devs\News4Devs.Client\wwwroot\profile-photos";
        
        public const string ImageExtension = ".png";

        public const int NotFound = 404;
        public const int Conflict = 409;
        public const int BadRequest = 400;

        public const string DevAPIBaseUrl = "https://dev.to/api/articles";
        public const string ITBookstoreAPIUrl = "https://api.itbook.store/1.0/";
        public const string QuotableAPIUrl = "https://api.quotable.io";

        public const int DefaultPageSize = 10;
        public const int DefaultPageNumber = 1;

        public const string ArticlesAddress = "https://localhost:44347/api/v1/articles/";
        public const string SavedArticlesEndpoint = "saved";
        public const string FavoriteArticlesEndpoint = "favorite";
    }
}
