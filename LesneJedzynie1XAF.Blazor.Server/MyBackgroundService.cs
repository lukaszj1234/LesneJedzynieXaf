using DevExpress.DataAccess.Native.Web;
using System.Net.Http;
using System.Security.Policy;

namespace LesneJedzynie1XAF.Blazor.Server
{
    public class MyBackgroundService : BackgroundService
    {
        readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(60));
        HttpClient _httpClient = new HttpClient();
        readonly string _url = "https://bernoulli-001-site1.dtempurl.com/Podlewaczka/GetOdczyt";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(await _timer.WaitForNextTickAsync(stoppingToken)
                  && !stoppingToken.IsCancellationRequested)
            {
                await DoWorkAsync();
            }
        }

        async Task DoWorkAsync()
        {
            var response = await _httpClient.GetAsync(_url);
        }
    }
}
