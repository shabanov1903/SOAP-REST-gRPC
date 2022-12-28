using PollyExample.Server;

namespace PollyExample.Subscriber.Services
{
    public interface IPollyServer
    {
        public PollyServer Server { get; }

        public Task<ICollection<WeatherForecast>> Get();
    }
}
