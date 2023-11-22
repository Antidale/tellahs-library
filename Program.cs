using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using tellahs_library.Commands;
using tellahs_library.Constants;
using tellahs_library.Services;

var token = Environment.GetEnvironmentVariable("DiscordBotToken");
var apiKey = Environment.GetEnvironmentVariable("ApiKey");

var discord = new DiscordClient(new DiscordConfiguration
{
    Token = token,
    TokenType = TokenType.Bot,
    Intents = DiscordIntents.AllUnprivileged,
});

var httpClient = new HttpClient { BaseAddress = new Uri("https://https://free-enterprise-info-api.herokuapp.com/api") };
httpClient.DefaultRequestHeaders.Add("X-API-Key", apiKey);

var slash = discord.UseSlashCommands(new SlashCommandsConfiguration
{
    Services = new ServiceCollection().AddTransient<RandomService>()
                                      .AddSingleton(service => httpClient)
                                      .BuildServiceProvider()
});

//Register test commands for the bot's server
slash.RegisterCommands<Tournament>(GuildIds.TestServer);
slash.RegisterCommands<TournamentRegistration>(GuildIds.AntiServer);

//Register global commands
slash.RegisterCommands<Recall>();
slash.RegisterCommands<BossRecall>();

await discord.ConnectAsync();
//check csharp fritz's discord bot vod for a better method of this
await Task.Delay(-1);
