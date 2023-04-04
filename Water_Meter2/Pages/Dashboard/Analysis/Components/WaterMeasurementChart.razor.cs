using AntDesign.Charts;
using Microsoft.AspNetCore.Components;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Water_Meter2.Services;

namespace Water_Meter2.Pages.Dashboard.Analysis
{
  public partial class WaterMeasurementChart
  {
    DateTime CurrentWeek;
    DateTime SelectedWeek;

    public WaterMeasurementChart()
    {
      CurrentWeek = DateTime.Now;
    }

    public void SetCurrentWeek()
    {
      CurrentWeek = SelectedWeek;
    }
  }
}
