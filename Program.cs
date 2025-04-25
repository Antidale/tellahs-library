using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using tellahs_library;
using tellahs_library.Helpers;

var token = SetupHelper.GetDiscordBotToken();

if (string.IsNullOrWhiteSpace(token))
    throw new NullReferenceException($"{nameof(token)} is invalid. Check environment variables");

var discordClient = DiscordClientBuilder
                .CreateDefault(token: token, intents: DiscordIntents.AllUnprivileged)
                .ConfigureServices(a => a
                    .AddLogging(log => log.AddConsole())
                    .AddSingleton(service => new FeInfoHttpClient())
                    .AddSingleton(service => new FeGenerationHttpClient()))
                .AddCommands()
                .Build();

await discordClient.ConnectAsync();

//TODO: check csharp fritz's discord bot vod for a better method of this
await Task.Delay(-1);
