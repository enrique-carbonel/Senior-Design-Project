using Microsoft.AspNetCore.Components;

namespace Water_Meter.Pages.Dashboard.Monitor
{
    public partial class Pie
    {
        [Parameter]
        public bool? Animate { get; set; }

        [Parameter]
        public int? LineWidth { get; set; }
    }
}