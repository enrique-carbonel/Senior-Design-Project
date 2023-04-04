using AntDesign.Charts;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Water_Meter2.Services;

namespace Water_Meter2.Pages.Dashboard.Analysis
{
    public partial class WaterMeasurementChart
    {

        DateTime SelectedWeek;
        [Inject] public IWaterChartService ChartService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            //await OnTabChanged("1");
        }

        //private async Task OnTabChanged(string activeKey)
        //{
        //    //var data = await ChartService.GetSalesDataAsync();
        //    //if (activeKey == "1")
        //    //    await _saleChart.ChangeData(data);
        //    //else
        //    //    await _visitChart.ChangeData(data);
        //}

        private void WeekChanged()
        { 
           
        }

    }
}
