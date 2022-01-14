using Microsoft.AspNetCore.Components;
using News4Devs.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News4Devs.Client.Components.People
{
    public partial class Persons
    {
        [Parameter]
        public List<UserDto> Users { get; set; } = new List<UserDto>();

        [Parameter]
        public bool isLoadMoreButtonVisible { get; set; }

        [Parameter]
        public EventCallback<ExtendedArticleDto> LoadMore { get; set; }

        private async Task OnLoadMore()
        {
            await LoadMore.InvokeAsync();
        }
    }
}
