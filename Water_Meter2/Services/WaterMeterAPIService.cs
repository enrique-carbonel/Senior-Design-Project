using System.Net.Http;

namespace Water_Meter2.Services
{
  public class WaterMeterAPIService
  {
    private readonly HttpClient httpClient;

    public WaterMeterAPIService(HttpClient httpClient)
    {
      this.httpClient = httpClient;
    }

    public string GetBaseUrl()
    {
      return httpClient.BaseAddress.ToString();
    }
  }

}
