using AntDesign;
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
    DateTime StartDate;
    DateTime EndDate;  

    public WaterMeasurementChart()
    {      
    }
    
    public void GetWeeklyData(DateTimeChangedEventArgs e)
    {  
      StartDate = e.Date;
      if (e.Date.DayOfWeek != DayOfWeek.Sunday)
      {
        StartDate = StartDate.AddDays(((int)e.Date.DayOfWeek) * -1);
      }
      EndDate = StartDate.AddDays(6);            
    }
  }
}
