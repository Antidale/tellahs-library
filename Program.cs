using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using tellahs_library.Commands;
using tellahs_library.Constants;

var token = Environment.GetEnvironmentVariable("DiscordBotToken");
var httpClient = new HttpClient { BaseAddress = new Uri("https://free-enterprise-info-api.herokuapp.com/api/") };
var apiKey = Environment.GetEnvironmentVariable("FE_Info_Api_Key");

#if DEBUG
token = Environment.GetEnvironmentVariable("TestBotToken");
apiKey = "test";
httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:5001/api/") };
#endif

if (token is null) { throw new ArgumentNullException(nameof(token)); }

httpClient.DefaultRequestHeaders.Add("Api-Key", apiKey);

var discord = DiscordClientBuilder
                .CreateDefault(token: token, intents: DiscordIntents.AllUnprivileged)
                .ConfigureServices(a => a
                    .AddLogging(log => log.AddConsole())
                    .AddSingleton(service => httpClient))
                .Build();

var commandsExtensions = discord.UseCommands(new CommandsConfiguration
{
    RegisterDefaultCommandProcessors = false
});

//Register test commands for the specific servers
await commandsExtensions.AddProcessorAsync(new SlashCommandProcessor());

commandsExtensions.AddCommands(typeof(FlagsetChooser));
commandsExtensions.AddCommands(typeof(Recall));

#if DEBUG
commandsExtensions.AddCommands(typeof(Tournament), GuildIds.AntiServer);
commandsExtensions.AddCommands(typeof(TournamentAdministration), GuildIds.AntiServer);
commandsExtensions.AddCommands(typeof(TournamentOverrides), GuildIds.AntiServer);
#else
commandsExtensions.AddCommands(typeof(Tournament), GuildIds.AntiServer, GuildIds.SideTourneyServer);
commandsExtensions.AddCommands(typeof(TournamentAdministration), GuildIds.AntiServer, GuildIds.SideTourneyServer);
commandsExtensions.AddCommands(typeof(TournamentOverrides), GuildIds.AntiServer, GuildIds.SideTourneyServer);
#endif


await discord.ConnectAsync();

//check csharp fritz's discord bot vod for a better method of this
await Task.Delay(-1);
