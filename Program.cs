using DSharpPlus;
using DSharpPlus.Extensions;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using tellahs_library;
using tellahs_library.Constants;
using tellahs_library.Helpers;
using tellahs_library.Services;

BoundUrlSettings boundUrlSettings = new();

var hostBuilder = Host.CreateApplicationBuilder()
                      .ConfigureEnvironmentVariables(boundUrlSettings)
                      .SetupSqlite();

hostBuilder.Logging.AddConsole()
                   .AddFilter("System.Net.Http", LogLevel.Error);

hostBuilder.Configuration.GetValueOrExit(ConfigKeys.FeInfoApiKey, out var apiKey)
                         .GetValueOrExit(ConfigKeys.FeInfoUrl, out var baseAddress)
                         .GetValueOrExit(ConfigKeys.DiscordToken, out var discordToken);

hostBuilder.Services.AddHttpClients(apiKey, new Uri(baseAddress))
                    .AddSingleton<ISqliteHelper, SqliteHelper>()
                    .AddSingleton(service => boundUrlSettings.ToUrlSettings())
                    .AddSingleton<ActiveRaces>()
                    .AddHostedService<DiscordBotService>()
                    .AddHostedService<RaceAnnouncerService>()
                    .AddDiscordClient(token: discordToken, intents: DiscordIntents.AllUnprivileged)
                    .AddInteractivityExtension(new DSharpPlus.Interactivity.InteractivityConfiguration
                    {
                        PollBehaviour = DSharpPlus.Interactivity.Enums.PollBehaviour.KeepEmojis,
                        Timeout = TimeSpan.FromMinutes(2)
                    })
                    .AddCommands();

using var cts = new CancellationTokenSource();
var token = cts.Token;
var app = hostBuilder.Build();
await app.RunAsync(token);
