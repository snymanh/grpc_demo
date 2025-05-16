using grpc_shared;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

namespace grpc_server.Services;

public class GreeterService(ILogger<GreeterService> logger) : Greeter.GreeterBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

    public override Task<WeatherForecastResponse> GetWeather(WeatherForecastRequest request, ServerCallContext context)
    {
        return Task.FromResult(new WeatherForecastResponse
        {
            Date = Timestamp.FromDateTime(DateTime.SpecifyKind(DateTime.Now.AddDays(1), DateTimeKind.Utc)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = "Freezing"
        });
    }
}