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
    enum DateRangeType
    {
      Daily,
      Weekly,
      Monthly
    }

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

    public async Task GetWeeklyData(DateTimeChangedEventArgs e)
    {
      StartDate = e.Date;
      GetDateRange(DateRangeType.Weekly);
      Weeklydata = await WaterChartService.GetWeeklyMeasurementDataAsync(StartDate, EndDate);
      await _waterChart.ChangeData(Weeklydata);
    }

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();
      await OnTabChanged("1");
    }

    private async Task OnTabChanged(string activeKey)
    {
      if (activeKey == "1")
      {
        await GetWeeklyData(new DateTimeChangedEventArgs());
      }

    }

    private void GetDateRange(DateRangeType rangeType)
    {

      switch (rangeType)
      {
        case DateRangeType.Daily:
          break;
        case DateRangeType.Weekly:
          if (StartDate == DateTime.MinValue)
          {
            StartDate = DateTime.Now;
          }
          if (StartDate.DayOfWeek != DayOfWeek.Sunday)
          {
            StartDate = StartDate.AddDays(((int)StartDate.DayOfWeek) * -1);
          }
          EndDate = StartDate.AddDays(6);
          break;
        case DateRangeType.Monthly:
          break;
        default:
          break;
      }
    }
  }
}
