namespace News4Devs.Core.Models
{
    // A model containing all the query params that can be used when calling Dev API
    public class DevApiQueryParamsModel
    {
        public string page { get; set; }

        public string per_page { get; set; }

        public string tag { get; set; }

        public string tags { get; set; }

        public string tags_exclude { get; set; }

        public string username { get; set; }

        public string state { get; set; }

        public string top { get; set; }
    }
}
