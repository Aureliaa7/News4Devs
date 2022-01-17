using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace News4Devs.Client.Shared
{
    public partial class LoadMoreButton
    {
        [Parameter]
        public EventCallback OnLoadMore { get; set; }

        [Parameter]
        public bool IsLoadMoreButtonVisible { get; set; }

        public async Task LoadMoreAsync()
        {
            await OnLoadMore.InvokeAsync();
        }
    }
}
