using System;
using DSharpPlus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace tellahs_library.Services;

public class DiscordBotService(ILogger<DiscordBotService> logger, DiscordClient client, IHostApplicationLifetime applicationLifetime) : IHostedService
{
    private readonly ILogger<DiscordBotService> _logger = logger;
    private readonly DiscordClient Client = client;
    private readonly IHostApplicationLifetime ApplicationLifetime = applicationLifetime;

    public async Task StartAsync(CancellationToken token)
    {
        await Client.ConnectAsync();
    }

    public async Task StopAsync(CancellationToken token)
    {
        _logger.LogInformation("bot stopping");
        await Client.DisconnectAsync();
    }

}
