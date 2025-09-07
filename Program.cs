using DSharpPlus;
using DSharpPlus.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using tellahs_library;
using tellahs_library.Constants;
using tellahs_library.RacingCommands;
using tellahs_library.Services;

BoundUrlSettings boundUrlSettings = new();

var hostBuilder = Host.CreateApplicationBuilder()
                      .ConfigureEnvironmentVariables(boundUrlSettings);

hostBuilder.Logging.AddConsole();

hostBuilder.Configuration.GetValueOrExit(ConfigKeys.FeInfoApiKey, out var apiKey)
                         .GetValueOrExit(ConfigKeys.FeInfoUrl, out var baseAddress)
                         .GetValueOrExit(ConfigKeys.DiscordToken, out var discordToken);

hostBuilder.Services.AddSingleton(service => new FeInfoHttpClient(apiKey, new Uri(baseAddress)))
                    .AddSingleton(service => new FeGenerationHttpClient())
                    .AddSingleton(service => new RacetimeHttpClient())
                    .AddSingleton(service => boundUrlSettings.ToUrlSettings())
                    .AddHostedService<DiscordBotService>()
                    .AddDiscordClient(token: discordToken, intents: DiscordIntents.AllUnprivileged)
                    .AddCommands();

var app = hostBuilder.Build();
await app.RunAsync();
