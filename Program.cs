using DSharpPlus;
using DSharpPlus.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using tellahs_library;
using tellahs_library.Constants;
using tellahs_library.RacingCommands;
using tellahs_library.Services;

var hostBuilder = Host.CreateApplicationBuilder()
                      .ConfigureEnvironmentVariables();

hostBuilder.Logging.AddConsole();

hostBuilder.Configuration.GetValueOrExit(ConfigKeys.FeInfoApiKey, out var apiKey)
                         .GetValueOrExit(ConfigKeys.FeInfoUrl, out var baseAddress)
#if DEBUG
                         .GetValueOrExit(ConfigKeys.DiscordDebugToken, out var discordToken);
#else
                         .GetValueOrExit(ConfigKeys.DiscordToken, out var discordToken);
#endif

hostBuilder.Services.AddSingleton(service => new FeInfoHttpClient())
                    .AddSingleton(service => new FeGenerationHttpClient())
                    .AddSingleton(service => new RacetimeHttpClient())
                    .AddHostedService<DiscordBotService>()
                    .AddDiscordClient(token: discordToken, intents: DiscordIntents.AllUnprivileged)
                    .AddCommands();

var stuff = hostBuilder.Build();
await stuff.RunAsync();
