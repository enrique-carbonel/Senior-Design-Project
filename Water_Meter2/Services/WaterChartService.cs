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

    public WaterChartService(HttpClient httpClient, IHttpClientFactory httpClientFactory)
    {
      _httpClient = httpClient;
      _httpClientFactory = httpClientFactory;
      _waterMeterAPIService = _httpClientFactory.CreateClient("WaterMeterAPIService");
      _jsonSerializerOptions = new JsonSerializerOptions();
      _jsonSerializerOptions.PropertyNameCaseInsensitive = true;
      _jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
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
        measurements = await _waterMeterAPIService.GetFromJsonAsync<List<Measurement>>(Uri,_jsonSerializerOptions);
      }
      catch (Exception ex)
      {
        var message = ex.Message;
      }
      
      
      data = measurements.GroupBy(m => m.TimeStamp.Date)
                         .Select(g => new ChartDataItem { X = g.Key.DayOfWeek.ToString(), Y = (int)g.Sum(m => m.Liters)})
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
