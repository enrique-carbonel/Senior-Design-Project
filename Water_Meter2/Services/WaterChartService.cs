using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Water_Meter2.Models;

namespace Water_Meter2.Services
{
    public interface IWaterChartService
    {
        Task<ChartDataItem[]> GetWeeklyMeasurementDataAsync();
        Task<ChartDataItem[]> GetVisitData2Async();
        Task<ChartDataItem[]> GetSalesDataAsync();
        Task<RadarDataItem[]> GetRadarDataAsync();
    }

    public class WaterChartService : IWaterChartService
    {
        private readonly HttpClient _httpClient;

        public WaterChartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChartDataItem[]> GetWeeklyMeasurementDataAsync()
        {
            return (await GetChartDataAsync()).VisitData;
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
