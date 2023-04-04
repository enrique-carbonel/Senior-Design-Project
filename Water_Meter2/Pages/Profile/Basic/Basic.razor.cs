using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Water_Meter2.Models;
using Water_Meter2.Services;

namespace Water_Meter2.Pages.Profile
{
    public partial class Basic
    {
        private BasicProfileDataType _data = new BasicProfileDataType();

        [Inject] protected IProfileService ProfileService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _data = await ProfileService.GetBasicAsync();
        }
    }
}