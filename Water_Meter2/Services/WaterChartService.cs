using AntDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Water_Meter2.Models;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Water_Meter2.Services
{
  public interface IWaterChartService
  {
    Task<ChartDataItem[]> GetWeeklyMeasurementDataAsync(DateTime StartDate, DateTime EndDate);
    Task<ChartDataItem[]> GetMonthlyMeasurementDataAsync(DateTime StartDate, DateTime EndDate);
    Task<ChartDataItem[]> GetMonthlyCostDataAsync(DateTime StartDate, DateTime EndDate);
    Task<ChartDataItem[]> GetVisitData2Async();
    Task<ChartDataItem[]> GetSalesDataAsync();
    Task<RadarDataItem[]> GetRadarDataAsync();
  }

  public class WaterChartService : IWaterChartService
  {
    private readonly HttpClient _httpClient;
    private readonly HttpClient _waterMeterAPIService;
    private readonly IHttpClientFactory _httpClientFactory;
    private JsonSerializerOptions _jsonSerializerOptions;
    Dictionary<int, double> WaterCost = new Dictionary<int, double>();

    public WaterChartService(HttpClient httpClient, IHttpClientFactory httpClientFactory)
    {
      _httpClient = httpClient;
      _httpClientFactory = httpClientFactory;
      _waterMeterAPIService = _httpClientFactory.CreateClient("WaterMeterAPIService");
      _jsonSerializerOptions = new JsonSerializerOptions();
      _jsonSerializerOptions.PropertyNameCaseInsensitive = true;
      _jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
      WaterCost.Add(4000, 0.907);
      WaterCost.Add(7000, 1.678);
      WaterCost.Add(12000, 3.039);
      WaterCost.Add(20000, 3.991);
      WaterCost.Add(20001, 5.669);
    }

    public async Task<ChartDataItem[]> GetWeeklyMeasurementDataAsync(DateTime StartDate, DateTime EndDate)
    {
      String Uri;
      List<Measurement> measurements = new List<Measurement>();
      ChartDataItem[] data;

      Uri = $"/Measurement/GetMeasurementByDate?StartDate={StartDate:yyyy-MM-dd}&EndDate={EndDate:yyyy-MM-dd}";
      //Fecha en yyyy-MM-dd
      try
      {
        measurements = await _waterMeterAPIService.GetFromJsonAsync<List<Measurement>>(Uri, _jsonSerializerOptions);
      }
      catch (Exception ex)
      {
        var message = ex.Message;
      }


      data = measurements.GroupBy(m => m.TimeStamp.Date)
                         .Select(g => new ChartDataItem { X = g.Key.DayOfWeek.ToString(), Y = (int)Math.Round(g.Average(m => m.Liters)) })
                         .ToArray();

      if (data.Length == 0)
      {

        for (DateTime date = StartDate; date <= EndDate; date = date.AddDays(1))
        {
          data = data.Append(new ChartDataItem { X = date.DayOfWeek.ToString(), Y = 0 });
        }
      }
      return (data);
    }

    public async Task<ChartDataItem[]> GetMonthlyMeasurementDataAsync(DateTime StartDate, DateTime EndDate)
    {
      String Uri;
      List<Measurement> measurements = new List<Measurement>();
      ChartDataItem[] data;

      Uri = $"/Measurement/GetMeasurementByDate?StartDate={StartDate:yyyy-MM-dd}&EndDate={EndDate:yyyy-MM-dd}";
      //Fecha en yyyy-MM-dd
      try
      {
        measurements = await _waterMeterAPIService.GetFromJsonAsync<List<Measurement>>(Uri, _jsonSerializerOptions);
      }
      catch (Exception ex)
      {
        var message = ex.Message;
      }


      data = measurements.GroupBy(m => m.TimeStamp.Month)
                         .Select(g => new ChartDataItem { X = (new DateTime(2023, g.Key, 1).ToString("MMM")), Y = (int)Math.Round(g.Average(m => m.Liters), 0) })
                         .ToArray();

      if (data.Length == 0)
      {

        for (DateTime date = StartDate; date <= EndDate; date = date.AddMonths(1))
        {
          data = data.Append(new ChartDataItem { X = date.ToString("MMM"), Y = 0 });
        }
      }
      return (data);
    }


    public async Task<ChartDataItem[]> GetMonthlyCostDataAsync(DateTime StartDate, DateTime EndDate)
    {
      String Uri;
      List<Measurement> measurements = new List<Measurement>();
      ChartDataItem[] data;


      Uri = $"/Measurement/GetMeasurementByDate?StartDate={StartDate:yyyy-MM-dd}&EndDate={EndDate:yyyy-MM-dd}";
      //Fecha en yyyy-MM-dd
      try
      {
        measurements = await _waterMeterAPIService.GetFromJsonAsync<List<Measurement>>(Uri, _jsonSerializerOptions);
      }
      catch (Exception ex)
      {
        var message = ex.Message;
      }


      data = measurements.GroupBy(m => m.TimeStamp.Month)
                         .Select(g => new ChartDataItem { X = (new DateTime(2023, g.Key, 1).ToString("MMM")), Y = (int)Math.Round(g.Average(m => m.Liters), 0) })
                         .ToArray();

      if (data.Length == 0)
      {

        for (DateTime date = StartDate; date <= EndDate; date = date.AddMonths(1))
        {
          data = data.Append(new ChartDataItem { X = date.ToString("MMM"), Y = 0 });
        }
      }
      foreach (var item in data)
      {
        var Cost = WaterCost.Where(x => x.Key > item.Y).OrderBy(x => x.Key).FirstOrDefault();
        if (Cost.Equals(default(KeyValuePair<int, double>)))
        {
          Cost = WaterCost.Last();
        }
        else 
        { 
          if (WaterCost.Any(x => x.Key < Cost.Key))
          {
            Cost = WaterCost.Where(x => x.Key < Cost.Key).OrderBy(x => x.Key).Last();
          }
        }
        item.Y = (int)Math.Round((item.Y/1000.0) * Cost.Value,0);
      }
        

      return (data);
    }


    public async Task<ChartDataItem[]> GetVisitData2Async()
    {
      return (await GetChartDataAsync()).VisitData2;
    }

    public async Task<ChartDataItem[]> GetSalesDataAsync()
    {
      return (await GetChartDataAsync()).SalesData;
    }

    public async Task<RadarDataItem[]> GetRadarDataAsync()
    {
      return (await GetChartDataAsync()).RadarData;
    }

    private async Task<ChartData> GetChartDataAsync()
    {
      return await _httpClient.GetFromJsonAsync<ChartData>("data/fake_chart_data.json");
    }
  }
}
