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
    ChartDataItem[] Monthlydata;
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

    public async Task GetMonthlyData(DateTimeChangedEventArgs e)
    {
            StartDate = e.Date;
            GetDateRange(DateRangeType.Monthly);
            Monthlydata = await WaterChartService.GetMonthlyMeasurementDataAsync(StartDate, EndDate);
            await _waterChart.ChangeData(Monthlydata);
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
      else if (activeKey == "2")
      {
        await GetMonthlyData(new DateTimeChangedEventArgs());
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
                    if(StartDate == DateTime.MinValue)
                    {
                        StartDate = DateTime.Now;
                    }
                    StartDate = new DateTime(StartDate.Year, 1, 1);
                    EndDate = new DateTime(StartDate.Year, 12, 31);
                    
          break;
        default:
          break;
      }
    }
  }
}
