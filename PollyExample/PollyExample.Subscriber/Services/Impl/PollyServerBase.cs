using PollyExample.Server;

namespace PollyExample.Subscriber.Services.Impl
{
    public class PollyServerBase : IPollyServer
    {
        #region Services

        private PollyServer _httpServer;

        #endregion

        public PollyServerBase(HttpClient httpClient)
        {
            _httpServer = new PollyServer("http://localhost:5272/", httpClient);
        }

        public PollyServer Server => _httpServer;

        public async Task<ICollection<WeatherForecast>> Get()
        {
            return await _httpServer.GetWeatherForecastAsync();
        }
    }
}
