﻿using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using tellahs_library.Commands;
using tellahs_library.Constants;
using tellahs_library.Services;

var token = Environment.GetEnvironmentVariable("DiscordBotToken");
var httpClient = new HttpClient { BaseAddress = new Uri("https://free-enterprise-info-api.herokuapp.com/api/") };
var apiKey = Environment.GetEnvironmentVariable("FE_Info_Api_Key");

#if DEBUG
token = Environment.GetEnvironmentVariable("TestBotToken");
apiKey = "test";
httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:5001/api/") };
#endif

if (token == null) { throw new ArgumentNullException(nameof(token)); }

httpClient.DefaultRequestHeaders.Add("Api-Key", apiKey);

var discord = new DiscordClient(new DiscordConfiguration
{
    Token = token,
    TokenType = TokenType.Bot,
    Intents = DiscordIntents.AllUnprivileged,
});

var slash = discord.UseSlashCommands(new SlashCommandsConfiguration
{
    Services = new ServiceCollection().AddSingleton<RandomService>()
                                      .AddSingleton(service => httpClient)
                                      .BuildServiceProvider()
});

//Register test commands for the specific servers

#if DEBUG
slash.RegisterCommands<Tournament>(GuildIds.AntiServer);
#else
slash.RegisterCommands<Tournament>(GuildIds.AntiServer);
slash.RegisterCommands<Tournament>(GuildIds.SideTourneyServer);
#endif

//Register global commands
slash.RegisterCommands<Recall>();
slash.RegisterCommands<FlagsetChooser>();

discord.ClientErrored += (DiscordClient sender, ClientErrorEventArgs args) => { discord.Logger.LogError(args.Exception.Message); return Task.CompletedTask; };
discord.SocketErrored += (DiscordClient sender, SocketErrorEventArgs args) => { discord.Logger.LogError(args.Exception.Message); return Task.CompletedTask; };

await discord.ConnectAsync();

//check csharp fritz's discord bot vod for a better method of this
await Task.Delay(-1);
