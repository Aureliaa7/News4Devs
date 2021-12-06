using Microsoft.AspNetCore.Components;
using News4Devs.Shared.DTOs;

namespace News4Devs.Client.Components.Books
{
    public partial class Book
    {
        [Parameter]
        public BookDto BookDto { get; set; }
    }
}
