using DSharpPlus;
using DSharpPlus.Extensions;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using tellahs_library;
using tellahs_library.Constants;
using tellahs_library.Services;

BoundUrlSettings boundUrlSettings = new();

var hostBuilder = Host.CreateApplicationBuilder()
                      .ConfigureEnvironmentVariables(boundUrlSettings);

var ActiveRaces = new ActiveRaces();

//fetch from local database/file to populate current idea of active races

hostBuilder.Logging.AddConsole();

hostBuilder.Configuration.GetValueOrExit(ConfigKeys.FeInfoApiKey, out var apiKey)
                         .GetValueOrExit(ConfigKeys.FeInfoUrl, out var baseAddress)
                         .GetValueOrExit(ConfigKeys.DiscordToken, out var discordToken);

hostBuilder.Services.AddSingleton(service => new FeInfoHttpClient(apiKey, new Uri(baseAddress)))
                    .AddSingleton(service => new FeGenerationHttpClient())
                    .AddSingleton(service => new RacetimeHttpClient())
                    .AddSingleton(service => boundUrlSettings.ToUrlSettings())
                    .AddSingleton(service => new ActiveRaces())
                    .AddHostedService<DiscordBotService>()
                    .AddDiscordClient(token: discordToken, intents: DiscordIntents.AllUnprivileged)
                    .AddInteractivityExtension(new DSharpPlus.Interactivity.InteractivityConfiguration
                    {
                        PollBehaviour = DSharpPlus.Interactivity.Enums.PollBehaviour.KeepEmojis,
                        Timeout = TimeSpan.FromMinutes(2)
                    })
                    .AddCommands()
                    .AddHttpClient();

var app = hostBuilder.Build();
await app.RunAsync();
