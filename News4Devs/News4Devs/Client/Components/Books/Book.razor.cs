using Microsoft.AspNetCore.Components;
using News4Devs.Core.DTOs;

namespace News4Devs.Client.Components.Books
{
    public partial class Book
    {
        [Parameter]
        public BookDto BookDto { get; set; }
    }
}
