namespace News4Devs.Shared.Models
{
    // A model containing all the query params that can be used when calling Quotable API
    public class QuotableApiQueryParamsModel
    {
        public int? maxLength { get; set; }

        public int? minLength { get; set; }

        public string tags { get; set; }

        public string author { get; set; }
    }
}
