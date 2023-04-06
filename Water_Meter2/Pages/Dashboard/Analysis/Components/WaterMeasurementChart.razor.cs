using AntDesign;
using AntDesign.Charts;
using Microsoft.AspNetCore.Components;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Water_Meter2.Models;
using Water_Meter2.Services;

namespace Water_Meter2.Pages.Dashboard.Analysis
{

  public partial class WaterMeasurementChart
  {
    DateTime StartDate;
    DateTime EndDate;
    ChartDataItem[] Weeklydata;

    private readonly ColumnConfig _WaterChartConfig = new ColumnConfig
    {
      Title = new AntDesign.Charts.Title
      {
        Visible = true,
        Text = "Water Measurement"
      },
      ForceFit = true,
      Padding = "auto",
      XField = "x",
      YField = "y"
    };

    private IChartComponent _waterChart;

    [Inject]
    public IWaterChartService WaterChartService { get; set; }



    public async void GetWeeklyData(DateTimeChangedEventArgs e)
    {
      StartDate = e.Date;
      Weeklydata = await WaterChartService.GetWeeklyMeasurementDataAsync(StartDate);
      await _waterChart.ChangeData(Weeklydata);
    }


    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      await OnTabChanged("1");
    }

    private async Task OnTabChanged(string activeKey)
    {
      Weeklydata = await WaterChartService.GetWeeklyMeasurementDataAsync(StartDate);
      if (activeKey == "1")
        await _waterChart.ChangeData(Weeklydata);
    }
  }
}
