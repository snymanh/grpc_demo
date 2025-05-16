using System.Diagnostics;
using grpc_shared;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

namespace grpc_rest_server.Controllers;

[ApiController]
[Route("GetWeatherForecast")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger, Greeter.GreeterClient client, HttpClient httpClient)
    : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    

    [HttpGet]
    public async Task<WeatherForecast> Get()
    {
        httpClient.BaseAddress = new Uri("http://localhost:5022");
        var result = await httpClient.GetAsync("/LazyController");
        var responseText = await result.Content.ReadFromJsonAsync<WeatherForecast>();
        return responseText;
        /*
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            */
    }
    
    [HttpGet("{name}")]
    public async Task<IActionResult> SayHelloAsync(string name)
    {
        var start = Stopwatch.StartNew();
        var reply = await client.GetWeatherAsync(new WeatherForecastRequest());
        start.Stop();
        Console.WriteLine($"gRPC call duration: {start.ElapsedMilliseconds}ms");
        return Ok(reply);
    }
}