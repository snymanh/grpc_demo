using Microsoft.AspNetCore.Mvc;

namespace grpc_rest_server.Controllers;

[ApiController]
[Route("LazyController")]
public class LazyController : ControllerBase
{
    [HttpGet]
    public WeatherForecast Get()
    {
        return new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = "Bracing"
        };
    }
}