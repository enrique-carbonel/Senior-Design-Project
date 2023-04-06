using AntDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Water_Meter2.Models;

namespace Water_Meter2.Services
{
  public interface IWaterChartService
  {
    Task<ChartDataItem[]> GetWeeklyMeasurementDataAsync(DateTime StartDate , DateTime EndDate );
    Task<ChartDataItem[]> GetVisitData2Async();
    Task<ChartDataItem[]> GetSalesDataAsync();
    Task<RadarDataItem[]> GetRadarDataAsync();
  }

  public class WaterChartService : IWaterChartService
  {
    private readonly HttpClient _httpClient;
    private readonly HttpClient _waterMeterAPIService;
    private readonly IHttpClientFactory _httpClientFactory;

    public WaterChartService(HttpClient httpClient, IHttpClientFactory httpClientFactory)
    {
      _httpClient = httpClient;
      _httpClientFactory = httpClientFactory;
      _waterMeterAPIService = _httpClientFactory.CreateClient("WaterMeterAPIService");
    }

    public async Task<ChartDataItem[]> GetWeeklyMeasurementDataAsync(DateTime StartDate, DateTime EndDate)
    {
      String Uri;
      List<Measurement> measurements = new List<Measurement>();
      ChartDataItem[] data;

      if (StartDate == DateTime.MinValue || EndDate == DateTime.MinValue) 
      {
        return (await GetChartDataAsync()).VisitData2;
      }
      //Uri = "/Measurement/GetMeasurementByDate?StartDate={}&EndDate={}";
      //Uri = "/Measurement/GetMeasurementByDate?StartDate=" + StartDate.ToString("yyyy/MM/dd");
      //Uri += "&EndDate=" + EndDate.ToString("yyyy/MM/dd");
      Uri = $"/Measurement/GetMeasurementByDate?StartDate={StartDate:yyyy/MM/dd}&EndDate={EndDate:yyyy/MM/dd}";
      //Fecha en AAAA/MM/DD
      measurements = await _waterMeterAPIService.GetFromJsonAsync<List<Measurement>> (Uri);
      data = measurements.GroupBy(m => m.TimeStamp.Date)
                         .Select(g => new ChartDataItem { X = g.Key.DayOfWeek.ToString(), Y = (int)g.Sum(m => m.Liters)})
                         .ToArray();
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
