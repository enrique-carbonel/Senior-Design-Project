using AntDesign.ProLayout;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Water_Meter2.Services;

namespace Water_Meter2
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      string baseaddress;
      builder.RootComponents.Add<App>("#app");

      baseaddress = builder.Configuration["BaseAddressURL"];

      builder.Services.AddHttpClient<WaterMeterAPIService>(client => client.BaseAddress = new Uri(baseaddress));
      builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
      builder.Services.AddAntDesign();
      builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
      builder.Services.AddScoped<IChartService, ChartService>();
      builder.Services.AddScoped<IWaterChartService, WaterChartService>();
      builder.Services.AddScoped<IProjectService, ProjectService>();
      builder.Services.AddScoped<IUserService, UserService>();
      builder.Services.AddScoped<IAccountService, AccountService>();
      builder.Services.AddScoped<IProfileService, ProfileService>();

      await builder.Build().RunAsync();
    }
  }
}