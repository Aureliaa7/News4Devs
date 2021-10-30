namespace News4Devs.Core.Models
{
    // A model containing all the query params that can be used when calling Dev API
    public class DevApiQueryParamsModel
    {
        public string Page { get; set; }

        public string Per_Page { get; set; }

        public string Tag { get; set; }

        public string Tags { get; set; }

        public string Tags_Exclude { get; set; }

        public string Username { get; set; }

        public string State { get; set; }

        public string Top { get; set; }
    }
}
