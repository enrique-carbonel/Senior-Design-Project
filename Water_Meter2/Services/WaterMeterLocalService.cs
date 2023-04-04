using System.Net.Http;

namespace Water_Meter2.Services
{
  public class WaterMeterLocalService
  {
    private readonly HttpClient httpClient;

    public WaterMeterLocalService(HttpClient httpClient)
    {
      this.httpClient = httpClient;
    }

    public string GetBaseUrl()
    {
      return httpClient.BaseAddress.ToString();
    }
  }

}
