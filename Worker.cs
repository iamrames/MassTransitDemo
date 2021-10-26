using MassTransit;
using Microsoft.Extensions.Logging;

namespace MassTransitDemo;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    readonly IBus _bus;

    public Worker(ILogger<Worker> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bus.Publish(new Message { Text = $"The time is {DateTimeOffset.Now}" });
            await Task.Delay(1000, stoppingToken);
        }
    }
}
