using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp_ClientPortal.Pages
{
    public partial class Dialog
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private void OnYes()
        {
            Title = "OnYes button is clicked!";
        }
    }
}
