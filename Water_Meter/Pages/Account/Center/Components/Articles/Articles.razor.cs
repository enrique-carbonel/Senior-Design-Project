using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Water_Meter.Models;

namespace Water_Meter.Pages.Account.Center
{
    public partial class Articles
    {
        [Parameter] public IList<ListItemDataType> List { get; set; }
    }
}