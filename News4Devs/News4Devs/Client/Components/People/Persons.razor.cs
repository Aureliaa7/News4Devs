using Microsoft.AspNetCore.Components;
using News4Devs.Shared.DTOs;
using System.Collections.Generic;

namespace News4Devs.Client.Components.People
{
    public partial class Persons
    {
        [Parameter]
        public List<UserDto> Users { get; set; } = new List<UserDto>();

        [Parameter]
        public bool isLoadMoreButtonVisible { get; set; }

        [Parameter]
        public EventCallback LoadMore { get; set; }
    }
}
