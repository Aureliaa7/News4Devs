using System.Collections.Generic;

namespace News4Devs.Shared.Pagination
{
    public class PagedResponseModel<T> where T: class, new()
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public string FirstPage { get; set; }
       
        public string LastPage { get; set; }

        public string NextPage { get; set; }

        public string PreviousPage { get; set; }

        public IList<T> Data { get; set; }
    }
}
