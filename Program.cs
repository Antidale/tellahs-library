using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using tellahs_library.Commands;
using tellahs_library.Constants;

/* TODO:
 add in httpClient for calling FE Api, probalby as a keyed service
 make current httpClient usage also a keyed service in DI
 add in config/environment variable info for FE's api key
 add in config/environment variable info for various endpoints (main, beta, forks)
 */


var token = Environment.GetEnvironmentVariable("DiscordBotToken");
var httpClient = new HttpClient { BaseAddress = new Uri("https://free-enterprise-info-api.herokuapp.com/api/") };
var apiKey = Environment.GetEnvironmentVariable("FE_Info_Api_Key");

#if DEBUG
token = Environment.GetEnvironmentVariable("TestBotToken");
apiKey = "test";
httpClient = new HttpClient
{
    BaseAddress = new Uri("https://localhost:5001/api/")
};
#endif


if (token is null) { throw new NullReferenceException($"{nameof(token)} is null. Check environment variables"); }

httpClient.DefaultRequestHeaders.Add("Api-Key", apiKey);

var discord = DiscordClientBuilder
                .CreateDefault(token: token, intents: DiscordIntents.AllUnprivileged)
                .ConfigureServices(a => a
                    .AddLogging(log => log.AddConsole())
                    .AddSingleton(service => httpClient));

var commandsConfig = new CommandsConfiguration
{
    RegisterDefaultCommandProcessors = false
};

discord.UseCommands((IServiceProvider ServiceProvider, CommandsExtension commands) =>
    {
        commands.AddProcessor(new SlashCommandProcessor());
        commands.AddCommands<FlagsetChooser>();
        commands.AddCommands<Recall>();

#if DEBUG
        commands.AddCommands<Tournament>(GuildIds.AntiServer);
        commands.AddCommands<TournamentAdministration>(GuildIds.AntiServer);
        commands.AddCommands<TournamentOverrides>(GuildIds.AntiServer);
#else
        commands.AddCommands<Tournament>(GuildIds.AntiServer, GuildIds.SideTourneyServer);
        commands.AddCommands<TournamentAdministration>(GuildIds.AntiServer, GuildIds.SideTourneyServer);
        commands.AddCommands<TournamentOverrides>(GuildIds.AntiServer, GuildIds.SideTourneyServer);
#endif
    },
    commandsConfig
);

discord.Build();

await discord.ConnectAsync();

//check csharp fritz's discord bot vod for a better method of this
await Task.Delay(-1);
