using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace News4Devs.Client.Shared
{
    public partial class SearchBar
    {
        [Parameter] 
        public EventCallback<string> OnSearch { get; set; }

        private bool shouldDisplaySearchBox = false;
        private bool showSearchIcon = true;
        private bool showHideText = false;
        private string searchedWords;

        private async Task SearchNews()
        {
            await OnSearch.InvokeAsync(searchedWords);
        }

        private void ShowSearchBox()
        {
            shouldDisplaySearchBox = true;
            SetShowHideOrShowSearchIcon();
        }

        private void ShowHideText()
        {
            shouldDisplaySearchBox = false;
            SetShowHideOrShowSearchIcon();
        }

        private void SetShowHideOrShowSearchIcon()
        {
            showSearchIcon = shouldDisplaySearchBox ? false : true;
            showHideText = shouldDisplaySearchBox ? true : false;
        }
    }
}
