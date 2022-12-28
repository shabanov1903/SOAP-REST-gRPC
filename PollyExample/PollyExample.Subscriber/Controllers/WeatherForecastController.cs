using Microsoft.AspNetCore.Mvc;
using PollyExample.Server;
using PollyExample.Subscriber.Services;

namespace PollyExample.Subscriber.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IPollyServer _pollyServer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPollyServer pollyServer)
        {
            _logger = logger;
            _pollyServer = pollyServer;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
        {
            var result = await _pollyServer.Get();
            return Ok(await _pollyServer.Get());
        }
    }
}
