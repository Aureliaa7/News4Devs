﻿namespace News4Devs.Shared.DTOs
{
    // Model used to get data from ITBookstore API
    public class BookDto
    {
        public string title { get; set; }

        public string subtitle { get; set; }

        public string image { get; set; }

        public string url { get; set; }

        public string isbn13 { get; set; }

        public string price { get; set; }
    }
}
