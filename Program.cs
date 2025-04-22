using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using tellahs_library.Helpers;

/* TODO:
 add in httpClient for calling FE Api, probalby as a keyed service
 make current httpClient usage also a keyed service in DI
 add in config/environment variable info for FE's api key
 add in config/environment variable info for various endpoints (main, beta, forks)
 */

var token = SetupHelper.GetDiscordBotToken();

if (token is null)
    throw new NullReferenceException($"{nameof(token)} is null. Check environment variables");

var discordClient = DiscordClientBuilder
                .CreateDefault(token: token, intents: DiscordIntents.AllUnprivileged)
                .ConfigureServices(a => a
                    .AddLogging(log => log.AddConsole())
                    .AddSingleton(service => new HttpClient().ConfigureForFeInfo()))
                .AddCommands()
                .Build();

await discordClient.ConnectAsync();

//TODO: check csharp fritz's discord bot vod for a better method of this
await Task.Delay(-1);
