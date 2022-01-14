namespace News4Devs.Client
{
    public static class ClientConstants
    {
        public const string BaseUrl = "https://localhost:44397/api/v1";
        public const string ContentType = "application/json";
        public const string ProfileImagesDirector = "/profile-photos/";
        public const string Token = "token";
        public const string ExpirationDateKey = "exp";
        
        public const int ImageWidth = 300;
        public const int ImageHeight = 500;

        public const int MaxPageSize = 15;

        public const int MaxQuoteLength = 65;
    }
}
