using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Water_Meter2.Models;

namespace Water_Meter2.Pages.Account.Center
{
    public partial class Articles
    {
        [Parameter] public IList<ListItemDataType> List { get; set; }
    }
}